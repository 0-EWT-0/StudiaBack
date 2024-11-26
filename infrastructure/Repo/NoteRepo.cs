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
            if (string.IsNullOrWhiteSpace(noteDTO.Name))
            {
                throw new InvalidOperationException("Note name cannot be null or empty.");
            }

            if (await NoteNameExistsAsync(noteDTO.Name))
            {
                throw new InvalidOperationException("A Note with the same name already exists for this folder.");
            }


            var note = new NoteEntity
            {
                name = noteDTO.Name,
                content = noteDTO.content,
                is_public = noteDTO.isPublic,
                id_folder_id = folderId,
                created_at = DateTime.UtcNow,
            };

            _dbContext.Notes.Add(note);
            await _dbContext.SaveChangesAsync();

            var response = "Note created succesfully";
            return new NoteResponse()
            {
                Response =  response,
                NoteId = note.id_note,
                FolderId = note.id_folder_id,
                Name = note.name,
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
            note.name = noteDTO.Name;
            note.content = noteDTO.newContent;
            note.is_public = noteDTO.isPublic;

            await _dbContext.SaveChangesAsync();

            var response = "Note updated succesfully";

            return new NoteResponse
            {
                Response = response,
                NoteId = note.id_note,
                FolderId = note.id_folder_id,
                Name = note.name,
                Content = note.content,
                IsPublic = note.is_public,
                CreatedAt = note.created_at
            };
        }

        // Método que actualiza las notas en la carpeta
        public async Task<bool> UpdateNotesFolderAsync(UpdateNotesFolderDTO updateNotesFolderDto)
        {
            // Buscar las notas con los Ids proporcionados
            var notes = await _dbContext.Notes
                .Where(n => updateNotesFolderDto.NoteIds.Contains(n.id_note))
                .ToListAsync();

            if (notes.Count == 0)
            {
                throw new InvalidOperationException("No notes found to update.");
            }

            // Actualizar la carpeta de las notas
            foreach (var note in notes)
            {
                note.id_folder_id = updateNotesFolderDto.FolderId;
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<NoteResponse> DeleteNoteAsync(int noteId)
        {
            var note = await _dbContext.Notes.FirstOrDefaultAsync(n => n.id_note == noteId);

            var response = "Note deleted succsesfully";

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
                Name = note.name,
                Content = note.content,
                IsPublic = note.is_public,
                CreatedAt = note.created_at
            };

        }

        public async Task<bool> NoteNameExistsAsync(string NoteName)
        {
            return await _dbContext.Notes
              .AnyAsync(n => n.name == NoteName);
        }
    }
}
