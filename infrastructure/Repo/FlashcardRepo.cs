using Application.Contracts;
using Application.DTOS;
using Application.DTOS.Responses;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class FlashcardRepo : IFlashcards
    {
        private readonly StudiaDBContext _dbContext;

        public FlashcardRepo(StudiaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<FlashcardResponse> CreateFlashcardAsync(CreateFlashcardDTO flashcardDTO, int userId)
        {
            var flashcard = new FlashcardEntity
            {
                content = flashcardDTO.content,
                is_public = flashcardDTO.isPublic,
                created_at = DateTime.UtcNow,
                id_user_id = userId,
                image_url = flashcardDTO.image_url
            };

            _dbContext.Flashcards.Add(flashcard);
            await _dbContext.SaveChangesAsync();

            return new FlashcardResponse
            {
                FlashcardId = flashcard.id_flashcard,
                Content = flashcard.content,
                IsPublic = flashcard.is_public,
                CreatedAt = flashcard.created_at,
                UserId = flashcard.id_user_id,
                ImageUrl = flashcard.image_url
            };
        }

        public async Task<FlashcardResponse> UpdateFlashcardAsync(UpdateFlashcardDTO flashcardDTO, int userId)
        {
            var flashcard = await _dbContext.Flashcards.FirstOrDefaultAsync(r => r.id_flashcard == flashcardDTO.FlashcardId && r.id_user_id == userId);

            if (flashcard == null)
            {
                throw new InvalidOperationException("flashcard not found or does not belong to the user");
            }

            flashcard.content = flashcardDTO.content;
            flashcard.is_public = flashcardDTO.isPublic;
            flashcard.image_url = flashcardDTO.image_url;

            await _dbContext.SaveChangesAsync();

            return new FlashcardResponse
            {
                FlashcardId = flashcard.id_flashcard,
                Content = flashcard.content,
                IsPublic = flashcard.is_public,
                CreatedAt = flashcard.created_at,
                UserId = flashcard.id_user_id,
                ImageUrl = flashcard.image_url
            };
        }

        public async Task<FlashcardResponse> DeleteFlashcardAsync(int flashcardId, int userId)
        {
            var flashcard = await _dbContext.Flashcards.FirstOrDefaultAsync(r => r.id_flashcard == flashcardId && r.id_user_id == userId);

            if (flashcard == null)
            {
                throw new InvalidOperationException("flashcard not found or does not belong to the user");
            }

            _dbContext.Flashcards.Remove(flashcard);
            await _dbContext.SaveChangesAsync();

            return new FlashcardResponse
            {
                FlashcardId = flashcard.id_flashcard,
                Content = flashcard.content,
                IsPublic = flashcard.is_public,
                CreatedAt = flashcard.created_at,
                UserId = flashcard.id_user_id,
                ImageUrl = flashcard.image_url,
                Response = "flashcard deleted successfully"
            };
        }

        public async Task<bool> FlashcardExistsAsync(string content, int userId)
        {
            return await _dbContext.Flashcards
                .AnyAsync(r => r.content == content && r.id_user_id == userId);
        }
    }
}
