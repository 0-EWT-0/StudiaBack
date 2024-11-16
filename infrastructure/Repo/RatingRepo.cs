using Application.Contracts;
using Application.DTOS;
using Application.DTOS.Responses;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo
{
    public class RatingRepo : IRating
    {
        private readonly StudiaDBContext _dbContext;

        public RatingRepo(StudiaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<RatingResponse> CreateRatingAsync(CreateRatingDTO ratingDTO, int userId)
        {
            if ((new[] { ratingDTO.FlashcardId, ratingDTO.ExamId, ratingDTO.ResumeId, ratingDTO.NoteId }).Count(id => id != null) != 1)
            {
                throw new InvalidOperationException("A rating must be associated with exactly one resource.");
            }

            if (userId <= 0)
            {
                throw new InvalidOperationException("User Unauthorized");
            }

            var rating = new RatingEntity
            {
                id_user_id = userId,
                id_flashcard_id = ratingDTO.FlashcardId ?? 0,
                id_exam_id = ratingDTO.ExamId ?? 0,
                id_resume_id = ratingDTO.ResumeId ?? 0,
                id_notes_id = ratingDTO.NoteId ?? 0,
                rating = ratingDTO.Rating,
                created_at = DateTime.UtcNow
            };

            _dbContext.Ratings.Add(rating);
            await _dbContext.SaveChangesAsync();

            var result = "Rating registered successfully";

            return new RatingResponse
            {
                Response = result,
                RatingId = rating.id_rating,
                UserId = rating.id_user_id,
                Rating = rating.rating,
                CreatedAt = rating.created_at
            };
        }


        public async Task<List<RatingResponse>> GetRatingsByResourceAsync(int? flashcardId, int? examId, int? resumeId, int? noteId)
        {
            var query = _dbContext.Ratings.AsQueryable();

            if (flashcardId.HasValue)
            {
                query = query.Where(r => r.id_flashcard_id == flashcardId.Value);
            }
            else if (examId.HasValue)
            {
                query = query.Where(r => r.id_exam_id == examId.Value);
            }
            else if (resumeId.HasValue)
            {
                query = query.Where(r => r.id_resume_id == resumeId.Value);
            }
            else if (noteId.HasValue)
            {
                query = query.Where(r => r.id_notes_id == noteId.Value);
            }
            else
            {
                throw new InvalidOperationException("At least one resource ID must be provided.");
            }

            return await query
                .Select(r => new RatingResponse
                {
                    RatingId = r.id_rating,
                    UserId = r.id_user_id,
                    Rating = r.rating,
                    CreatedAt = r.created_at
                })
                .ToListAsync();
        }

    }
}
