
using Application.DTOS;
using Application.DTOS.Responses;

namespace Application.Contracts
{
    public interface INotes
    {
        Task<NoteResponse> CreateNoteAsync(CreateNoteDTO noteDTO, int folder);

        Task<NoteResponse> UpdateNoteAsync(UpdateNoteDTO noteDTO);

        Task<bool> UpdateNotesFolderAsync(UpdateNotesFolderDTO updateNotesFolderDto);


        Task<NoteResponse> DeleteNoteAsync(int noteId);

        Task<DeleteMultipleNotesResponse> DeleteMultipleNotesAsync(List<int> noteIds); // Nueva declaración

        Task<bool> NoteNameExistsAsync(string NoteName);
    }
}
