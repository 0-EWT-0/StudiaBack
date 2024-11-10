

using Application.DTOS;

namespace Application.Contracts
{
    public interface INotes
    {
        Task<NoteResponse> CreateNoteAsync(CreateNoteDTO noteDTO, int folder);
    }
}
