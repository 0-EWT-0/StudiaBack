using Application.Contracts;
using Application.DTOS;
using Domain.Entities;
using Infrastructure.Data;
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
    }
}
