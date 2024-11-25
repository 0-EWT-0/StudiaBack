using Application.Contracts;
using Application.DTOS.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo
{
    public class MaterialRepo : IMaterials
    {
        private readonly StudiaDBContext _dbContext;

        public MaterialRepo(StudiaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MaterialResponse>> GetAllResources()
        {
            return await _dbContext.Materials
                .Include(m => m.Exam)
                .Include(m => m.Flashcard)
                .Include(m => m.Resume)
                .Select(m => new MaterialResponse
                {
                    MaterialId = m.id_material,
                    UserId = m.id_user_id,
                    Exam = m.Exam != null ? new ExamResponse
                    {
                        ExamId = m.Exam.id_exam,
                        Name = m.Exam.name,
                        Content = m.Exam.content,
                        ImageUrl = m.Exam.image_url,
                        Difficulty = m.Exam.difficulty,
                        IsPublic = m.Exam.is_public,
                        CreatedAt = m.Exam.created_at
                    } : null,
                    Flashcard = m.Flashcard != null ? new FlashcardResponse
                    {
                        FlashcardId = m.Flashcard.id_flashcard,
                        Name = m.Flashcard.name,
                        Content = m.Flashcard.content,
                        ImageUrl = m.Flashcard.image_url,
                        IsPublic = m.Flashcard.is_public,
                        CreatedAt = m.Flashcard.created_at
                    } : null,
                    Resume = m.Resume != null ? new ResumeResponse
                    {
                        ResumeId = m.Resume.id_resume,
                        Content = m.Resume.content,
                        IsPublic = m.Resume.is_public,
                        CreatedAt = m.Resume.created_at
                    } : null,
                })
                .ToListAsync();
        }

        public async Task<List<MaterialResponse>> GetMaterialsByUserIdAsync(int userId)
        {
            return await _dbContext.Materials
                .Where(m => m.id_user_id == userId)
                .Include(m => m.Exam)
                .Include(m => m.Flashcard)
                .Include(m => m.Resume)
                .Select(m => new MaterialResponse
                {
                    MaterialId = m.id_material,
                    UserId = m.id_user_id,
                    Exam = m.Exam != null ? new ExamResponse
                    {
                        ExamId = m.Exam.id_exam,
                        Name = m.Exam.name,
                        Content = m.Exam.content,
                        ImageUrl = m.Exam.image_url,
                        Difficulty = m.Exam.difficulty,
                        IsPublic = m.Exam.is_public,
                        CreatedAt = m.Exam.created_at
                    } : null,
                    Flashcard = m.Flashcard != null ? new FlashcardResponse
                    {
                        FlashcardId = m.Flashcard.id_flashcard,
                        Name = m.Flashcard.name,
                        Content = m.Flashcard.content,
                        ImageUrl = m.Flashcard.image_url,
                        IsPublic = m.Flashcard.is_public,
                        CreatedAt = m.Flashcard.created_at
                    } : null,
                    Resume = m.Resume != null ? new ResumeResponse
                    {
                        ResumeId = m.Resume.id_resume,
                        Content = m.Resume.content,
                        IsPublic = m.Resume.is_public,
                        CreatedAt = m.Resume.created_at
                    } : null,
                })
                .ToListAsync();
        }


    }
}
