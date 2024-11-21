using Application.Contracts;
using Application.DTOS;
using Application.DTOS.Responses;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class ResumeRepo : IResumes
    {
        private readonly StudiaDBContext _dbContext;

        public ResumeRepo(StudiaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResumeResponse> CreateResumeAsync(CreateResumeDTO resumeDTO, int userId)
        {
            var resume = new ResumeEntity
            {
                content = resumeDTO.content,
                is_public = resumeDTO.isPublic,
                created_at = DateTime.UtcNow,
                id_user_id = userId,
                image_url = resumeDTO.image_url,
                id_type_id = resumeDTO.typeId
            };

            _dbContext.Resumes.Add(resume);
            await _dbContext.SaveChangesAsync();

            var material = new MaterialEntity
            {
                id_user_id = userId,
                id_resume_id = resume.id_resume,
                created_at = DateTime.UtcNow
            };

            _dbContext.Materials.Add(material);
            await _dbContext.SaveChangesAsync();

            return new ResumeResponse
            {
                ResumeId = resume.id_resume,
                Content = resume.content,
                IsPublic = resume.is_public,
                CreatedAt = resume.created_at,
                UserId = resume.id_user_id,
                ImageUrl = resume.image_url,
                TypeId = resume.id_type_id
            };
        }

        public async Task<ResumeResponse> UpdateResumeAsync(UpdateResumeDTO resumeDTO, int userId, int resumeId)
        {
            var resume = await _dbContext.Resumes.FirstOrDefaultAsync(r => r.id_resume == resumeId && r.id_user_id == userId);

            if (resume == null)
            {
                throw new InvalidOperationException("Resume not found or does not belong to the user");
            }

            resume.content = resumeDTO.content;
            resume.is_public = resumeDTO.isPublic;
            resume.image_url = resumeDTO.image_url;

            await _dbContext.SaveChangesAsync();

            return new ResumeResponse
            {
                ResumeId = resume.id_resume,
                Content = resume.content,
                IsPublic = resume.is_public,
                CreatedAt = resume.created_at,
                UserId = resume.id_user_id,
                ImageUrl = resume.image_url,
                TypeId = resume.id_type_id
            };
        }

        public async Task<ResumeResponse> DeleteResumeAsync(int resumeId, int userId)
        {
            var resume = await _dbContext.Resumes.FirstOrDefaultAsync(r => r.id_resume == resumeId && r.id_user_id == userId);

            if (resume == null)
            {
                throw new InvalidOperationException("Resume not found or does not belong to the user");
            }

            _dbContext.Resumes.Remove(resume);
            await _dbContext.SaveChangesAsync();

            return new ResumeResponse
            {
                ResumeId = resume.id_resume,
                Content = resume.content,
                IsPublic = resume.is_public,
                CreatedAt = resume.created_at,
                UserId = resume.id_user_id,
                ImageUrl = resume.image_url,
                TypeId = resume.id_type_id,

                Response = "Resume deleted successfully"
            };
        }

    }
}
