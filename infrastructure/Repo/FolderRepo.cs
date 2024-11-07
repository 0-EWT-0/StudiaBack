using Application.Contracts;
using Application.DTOS;
using Domain.Entities;
using Infrastructure.Data;
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
                throw new InvalidOperationException("A folder with the same name already exists.");
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

            return new FolderResponse
            {
                UserId = folder.id_user_id,
                FolderId = folder.id_folder,
                Name = folder.name,
                IsPublic = folder.is_public,
                CreatedAt = folder.created_at
            };
        }
    }
}
