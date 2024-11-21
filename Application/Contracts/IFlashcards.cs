using Application.DTOS.Responses;
using Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Contracts
{
    public interface IFlashcards
    {
        Task<FlashcardResponse> CreateFlashcardAsync(CreateFlashcardDTO flashcardDTO, int userId);

        Task<FlashcardResponse> UpdateFlashcardAsync(UpdateFlashcardDTO flashcardDTO, int userId, int flashcardId);

        Task<FlashcardResponse> DeleteFlashcardAsync(int flashcardId, int userId);

        Task<bool> FlashcardNameExistsAsync(string flashcardName, int userId);

    }
}
