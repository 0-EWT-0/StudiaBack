using Application.Contracts;
using Application.DTOS;
using Application.DTOS.Responses;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class FolderRepo : IFolders
    {
        private readonly StudiaDBContext _dbContext;

        public FolderRepo(StudiaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

       
        public async Task<bool> FolderExistsAsync(string folderName, int userId)
        {
            return await _dbContext.Folders
                .AnyAsync(f => f.name == folderName && f.id_user_id == userId);
        }
        public async Task<FolderResponse> CreateFolderAsync(CreateFolderDTO folderDTO, int userId)
        {
            if (await FolderExistsAsync(folderDTO.name, userId))
            {
                throw new InvalidOperationException("User is not auyenticated");
            }

            var folder = new FolderEntity
            {
                name = folderDTO.name,
                is_public = folderDTO.isPublic,
                id_user_id = userId,
                created_at = DateTime.UtcNow
            };

            _dbContext.Folders.Add(folder);
            await _dbContext.SaveChangesAsync();

            var result = "folder created succesfully";

            return new FolderResponse
            {
                Response = result,
                UserId = folder.id_user_id,
                FolderId = folder.id_folder,
                Name = folder.name,
                IsPublic = folder.is_public,
                CreatedAt = folder.created_at
            };
        }

        public async Task<FolderResponse> DeleteFolderAsync(int folderId, int userId)
        {
            // Verificar si el folder existe y pertenece al usuario
            var folder = await _dbContext.Folders
                .Include(f => f.Notes)
                .FirstOrDefaultAsync(f => f.id_folder == folderId && f.id_user_id == userId);

            if (folder == null)
            {
                throw new InvalidOperationException("Folder not found or does not belong to the user.");
            }

            // Eliminar las notas asociadas
            if (folder.Notes.Any())
            {
                _dbContext.Notes.RemoveRange(folder.Notes);
            }

            // Eliminar el folder
            _dbContext.Folders.Remove(folder);
            await _dbContext.SaveChangesAsync();

            return new FolderResponse
            {
                UserId = folder.id_user_id,
                FolderId = folder.id_folder,
                Name = folder.name,
                IsPublic = folder.is_public,
                CreatedAt = folder.created_at,
                Response = "Folder deleted successfully."
            };
        }


        public async Task<DeleteMultipleFoldersResponse> DeleteMultipleFoldersAsync(List<int> folderIds)
        {
            // Verificar si los folders existen y pertenecen al usuario
            var folders = await _dbContext.Folders
                .Where(f => folderIds.Contains(f.id_folder))
                .Include(f => f.Notes)
                .ToListAsync();

            if (folders.Count == 0)
            {
                throw new InvalidOperationException("No folders found to delete.");
            }

            // Eliminar las notas asociadas
            foreach (var folder in folders)
            {
                if (folder.Notes.Any())
                {
                    _dbContext.Notes.RemoveRange(folder.Notes);
                }
            }

            // Eliminar los folders
            _dbContext.Folders.RemoveRange(folders);
            await _dbContext.SaveChangesAsync();

            return new DeleteMultipleFoldersResponse
            {
                FolderIds = folders.Select(f => f.id_folder).ToList(),
                Response = "Folders deleted successfully."
            };
        }


        public async Task<FolderResponse> UpdateFolderAsync(UpdateFolderDTO folderDTO, int userId)
        {
            var folder = await _dbContext.Folders.FirstOrDefaultAsync(f => f.id_folder == folderDTO.folderId && f.id_user_id == userId);

            var response = "folder updated succsesfully";

            if (folder == null)
            {
                throw new InvalidOperationException("Folder not found");
            }

            folder.name = folderDTO.newName;

            folder.is_public = folderDTO.isPublic;

            await _dbContext.SaveChangesAsync();

            return new FolderResponse
            {
                Response = response,
                UserId= folder.id_user_id,
                FolderId = folder.id_folder,
                Name = folder.name,
                IsPublic = folder.is_public,
                CreatedAt = folder.created_at
            };
        }

        public async Task<List<FolderResponse>> GetFoldersAsync(int userId)
        {
            // Filtrar folders que pertenezcan al usuario y mapear a FolderResponse
            return await _dbContext.Folders
                .Where(f => f.id_user_id == userId)
                .Include(f => f.Notes) // Incluir las notas asociadas
                .Select(f => new FolderResponse
                {
                    FolderId = f.id_folder,
                    UserId = f.id_user_id,
                    Name = f.name,
                    IsPublic = f.is_public,
                    CreatedAt = f.created_at,
                    Notes = f.Notes.Select(note => new NoteResponse
                    {
                        NoteId = note.id_note,
                        Name = note.name,
                        Content = note.content,
                        IsPublic = note.is_public,
                        CreatedAt = note.created_at
                    }).ToList()
                })
                .ToListAsync();
        }


    }
}
