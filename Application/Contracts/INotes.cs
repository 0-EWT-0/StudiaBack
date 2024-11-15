
using Application.DTOS;
using Application.DTOS.Responses;

namespace Application.Contracts
{
    public interface INotes
    {
        Task<NoteResponse> CreateNoteAsync(CreateNoteDTO noteDTO, int folder);

        Task<NoteResponse> UpdateNoteAsync(UpdateNoteDTO noteDTO);

        Task<NoteResponse> DeleteNoteAsync(int noteId);
    }
}
