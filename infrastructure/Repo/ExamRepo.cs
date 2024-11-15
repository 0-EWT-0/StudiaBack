
using Application.Contracts;
using Application.DTOS;
using Application.DTOS.Responses;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo
{
    public class ExamRepo : IExams
    {
        private readonly StudiaDBContext _dbContext;

        public ExamRepo(StudiaDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<ExamResponse> CreateExamAsync(CreateExamDTO examDTO, int userId)
        {
            var exam = new ExamEntity
            {
                id_user_id = userId,
                content = examDTO.Content,
                image_url = examDTO.Image_url,
                difficulty = examDTO.Difficulty,
                is_public = examDTO.IsPublic,
                created_at = DateTime.UtcNow,
                id_type_id = examDTO.TypeId
            };

            _dbContext.Exams.Add(exam);
            await _dbContext.SaveChangesAsync();

            var result = "Exam created successfully";

            return new ExamResponse
            {
                Response = result,
                ExamId = exam.id_exam,
                UserId = exam.id_user_id,
                TypeId = exam.id_type_id,
                Content = exam.content,
                ImageUrl = exam.image_url,
                Difficulty = exam.difficulty,
            };
        }


        public async Task<ExamResponse> DeleteExamAsync(int examId , int userId)
        {
            var exam = await _dbContext.Exams.FirstOrDefaultAsync(e => e.id_exam == examId && e.id_user_id == userId);

            if (exam == null)
            {
                throw new InvalidOperationException("Exam not found or user is not authorized.");
            }

            _dbContext.Exams.Remove(exam);
            await _dbContext.SaveChangesAsync();

            var result = "Exam deleted successfully";

            return new ExamResponse
            {
                Response = result,
                ExamId = exam.id_exam,
                UserId = exam.id_user_id,
                TypeId = exam.id_type_id,
                Content = exam.content,
                ImageUrl = exam.image_url,
                Difficulty = exam.difficulty,
            };
        }


        public async Task<ExamResponse> UpdateExamAsync(UpdateExamDTO examDTO, int userId)
        {
            var exam = await _dbContext.Exams.FirstOrDefaultAsync(e => e.id_exam == examDTO.ExamId && e.id_user_id == userId);

            if (exam == null)
            {
                throw new InvalidOperationException("Exam not found or user is not authorized.");
            }

            exam.content = examDTO.Content;
            exam.image_url = examDTO.ImageUrl;
            exam.difficulty = examDTO.Difficulty;
            exam.is_public = examDTO.IsPublic;

            await _dbContext.SaveChangesAsync();

            var result = "Exam updated successfully";

            return new ExamResponse
            {
                Response = result,
                ExamId = exam.id_exam,
                UserId = exam.id_user_id,
                TypeId = exam.id_type_id,
                Content = exam.content,
                ImageUrl = exam.image_url,
                Difficulty = exam.difficulty,
            };
        }
    }
}
