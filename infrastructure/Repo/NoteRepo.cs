using Application.Contracts;
using Application.DTOS;
using Application.DTOS.Responses;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class NoteRepo : INotes
    {
        private readonly StudiaDBContext _dbContext;

        public NoteRepo(StudiaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<NoteResponse> CreateNoteAsync(CreateNoteDTO noteDTO, int folderId)
        {
            var note = new NoteEntity
            {
                content = noteDTO.content,
                is_public = noteDTO.isPublic,
                id_folder_id = folderId,
                created_at = DateTime.UtcNow,
            };

            _dbContext.Notes.Add(note);
            await _dbContext.SaveChangesAsync();

            return new NoteResponse()
            {
                NoteId = note.id_note,
                FolderId = note.id_folder_id,
                Content = note.content,
                IsPublic = note.is_public,
                CreatedAt = note.created_at
            };
        }

        public async Task<NoteResponse> UpdateNoteAsync(UpdateNoteDTO noteDTO)
        {
            var note = await _dbContext.Notes.Include(f => f.Folder).FirstOrDefaultAsync(n => n.id_note == noteDTO.noteId);

            if (note == null)
            {
                throw new InvalidOperationException("Note not found");
            }

            note.content = noteDTO.newContent;

            note.is_public = noteDTO.isPublic;

            await _dbContext.SaveChangesAsync();

            return new NoteResponse
            {
                NoteId = note.id_note,
                FolderId = note.id_folder_id,
                Content = note.content,
                IsPublic = note.is_public,
                CreatedAt = note.created_at
            };
        }

        public async Task<NoteResponse> DeleteNoteAsync(int noteId)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(n => n.id_note == noteId);

            var response = "folder deleted succsesfully";

            if (note == null)
            {
                throw new InvalidOperationException("Note not found");
            }

            _dbContext.Notes.Remove(note);

            await _dbContext.SaveChangesAsync();

            return new NoteResponse
            {
                Response = response,
                NoteId = noteId,
                Content = note.content,
                IsPublic = note.is_public,
                CreatedAt = note.created_at
            };

        }
    }
}
