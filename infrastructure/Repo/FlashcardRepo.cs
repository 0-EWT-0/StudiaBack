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
            if (string.IsNullOrWhiteSpace(flashcardDTO.Name))
            {
                throw new InvalidOperationException("Flashcard name cannot be null or empty.");
            }

            if (flashcardDTO.typeId <= 0)
            {
                throw new InvalidOperationException("Flashcard typeId must be a positive value.");
            }

            if (await FlashcardNameExistsAsync(flashcardDTO.Name, userId))
            {
                throw new InvalidOperationException("A flashcard with the same name already exists for this user.");
            }
            var flashcard = new FlashcardEntity
            {
                name = flashcardDTO.Name,
                content = flashcardDTO.content,
                is_public = flashcardDTO.isPublic,
                created_at = DateTime.UtcNow,
                id_user_id = userId,
                image_url = flashcardDTO.image_url,
                id_type_id = flashcardDTO.typeId
            };

            if (flashcard.id_type_id == 0)
            {
                throw new InvalidOperationException("Flashcard typeid needs to be either 1 or 3");
            }

            _dbContext.Flashcards.Add(flashcard);
            await _dbContext.SaveChangesAsync();

            var material = new MaterialEntity
            {
                id_user_id = userId,
                id_flashcard_id = flashcard.id_flashcard,
                created_at = DateTime.UtcNow
            };

            _dbContext.Materials.Add(material);
            await _dbContext.SaveChangesAsync();

            var response = "flashcard deleted successfully";
            return new FlashcardResponse
            {
                Response = response,
                FlashcardId = flashcard.id_flashcard,
                Name = flashcard.name,
                Content = flashcard.content,
                IsPublic = flashcard.is_public,
                CreatedAt = flashcard.created_at,
                UserId = flashcard.id_user_id,
                ImageUrl = flashcard.image_url,
                TypeId = flashcard.id_type_id
            };
        }

        public async Task<FlashcardResponse> UpdateFlashcardAsync(UpdateFlashcardDTO flashcardDTO, int userId, int flashcardId)
        {
            var flashcard = await _dbContext.Flashcards.FirstOrDefaultAsync(r => r.id_flashcard == flashcardId && r.id_user_id == userId);

            if (flashcard == null)
            {
                throw new InvalidOperationException("flashcard not found or does not belong to the user");
            }

            flashcard.name = flashcardDTO.Name;
            flashcard.content = flashcardDTO.content;
            flashcard.is_public = flashcardDTO.isPublic;
            flashcard.image_url = flashcardDTO.image_url;

            await _dbContext.SaveChangesAsync();

            var response = "flashcard updated successfully";
            return new FlashcardResponse
            {
                Response = response,
                FlashcardId = flashcard.id_flashcard,
                Name = flashcard.name,
                Content = flashcard.content,
                IsPublic = flashcard.is_public,
                CreatedAt = flashcard.created_at,
                UserId = flashcard.id_user_id,
                ImageUrl = flashcard.image_url,
                TypeId = flashcard.id_type_id,
            };
        }

        public async Task<FlashcardResponse> DeleteFlashcardAsync(int flashcardId, int userId)
        {
            var flashcard = await _dbContext.Flashcards.FirstOrDefaultAsync(r => r.id_flashcard == flashcardId && r.id_user_id == userId);

            if (flashcard == null)
            {
                throw new InvalidOperationException("flashcard not found or does not belong to the user");
            }
            // eliminar relacion de materials
            var relatedMaterials = _dbContext.Materials.Where(m => m.id_flashcard_id == flashcardId);
            _dbContext.Materials.RemoveRange(relatedMaterials);

            _dbContext.Flashcards.Remove(flashcard);
            await _dbContext.SaveChangesAsync();

            var response = "flashcard deleted successfully";
            return new FlashcardResponse
            {
                Response = response,
                FlashcardId = flashcard.id_flashcard,
                Content = flashcard.content,
                IsPublic = flashcard.is_public,
                CreatedAt = flashcard.created_at,
                UserId = flashcard.id_user_id,
                ImageUrl = flashcard.image_url,
                TypeId = flashcard.id_type_id,
            };
        }

        //public async Task<bool> FlashcardExistsAsync(string content, int userId)
        //{
        //    return await _dbContext.Flashcards
        //        .AnyAsync(r => r.content == content && r.id_user_id == userId);
        //}

        public async Task<List<FlashcardEntity>> GetUserFlashcardsAsync(int userId) 
        { 
            return await _dbContext.Flashcards.Where(r => r.id_user_id == userId).ToListAsync(); 
        }

        public async Task<bool> FlashcardNameExistsAsync(string flashcardName, int userId)
        {
            return await _dbContext.Flashcards
             .AnyAsync(f => f.name == flashcardName && f.id_user_id == userId);
        }
    }
}
