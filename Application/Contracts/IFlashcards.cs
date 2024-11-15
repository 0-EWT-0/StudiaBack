using Application.DTOS.Responses;
using Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IFlashcards
    {
        Task<FlashcardResponse> CreateFlashcardAsync(CreateFlashcardDTO flashcardDTO, int userId);

        Task<FlashcardResponse> UpdateFlashcardAsync(UpdateFlashcardDTO flashcardDTO, int userId);

        Task<FlashcardResponse> DeleteFlashcardAsync(int flashcardId, int userId);
        Task<bool> FlashcardExistsAsync(string content, int userId);
    }
}
