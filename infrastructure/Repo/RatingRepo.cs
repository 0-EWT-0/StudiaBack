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
            bool alreadyRated = await _dbContext.Ratings.AnyAsync(r =>
            r.id_user_id == userId &&
           (r.id_flashcard_id == (ratingDTO.FlashcardId ?? 0) ||
            r.id_exam_id == (ratingDTO.ExamId ?? 0) ||
            r.id_resume_id == (ratingDTO.ResumeId ?? 0) ||
            r.id_notes_id == (ratingDTO.NoteId ?? 0)));

            if (alreadyRated)
            {
                throw new InvalidOperationException("You have already rated this resource.");
            }

            if (ratingDTO.FlashcardId.HasValue)
            {
                var flashcard = await _dbContext.Flashcards.FirstOrDefaultAsync(f => f.id_flashcard == ratingDTO.FlashcardId.Value);
                if (flashcard == null || !flashcard.is_public)
                {
                    throw new InvalidOperationException("The flashcard is either not found or not public.");
                }
            }
            else if (ratingDTO.ExamId.HasValue)
            {
                var exam = await _dbContext.Exams.FirstOrDefaultAsync(e => e.id_exam == ratingDTO.ExamId.Value);
                if (exam == null || !exam.is_public)
                {
                    throw new InvalidOperationException("The exam is either not found or not public.");
                }
            }
            else if (ratingDTO.ResumeId.HasValue)
            {
                var resume = await _dbContext.Resumes.FirstOrDefaultAsync(r => r.id_resume == ratingDTO.ResumeId.Value);
                if (resume == null || !resume.is_public)
                {
                    throw new InvalidOperationException("The resume is either not found or not public.");
                }
            }
            else if (ratingDTO.NoteId.HasValue)
            {
                var note = await _dbContext.Notes.FirstOrDefaultAsync(n => n.id_note == ratingDTO.NoteId.Value);
                if (note == null || !note.is_public)
                {
                    throw new InvalidOperationException("The note is either not found or not public.");
                }
            }
            else
            {
                throw new InvalidOperationException("A rating must be associated with exactly one resource.");
            }


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
                id_flashcard_id = ratingDTO.FlashcardId,
                id_exam_id = ratingDTO.ExamId,
                id_resume_id = ratingDTO.ResumeId,
                id_notes_id = ratingDTO.NoteId,
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
                //NoteId = rating.id_notes_id,
                //FlaschardId = rating.id_flashcard_id,
                //ExamId = rating.id_exam_id,
                //ResumeId = rating.id_resume_id,
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
