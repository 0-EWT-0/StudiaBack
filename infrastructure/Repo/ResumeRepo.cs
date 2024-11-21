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
            if (string.IsNullOrWhiteSpace(resumeDTO.Name))
            {
                throw new InvalidOperationException("Resume name cannot be null or empty.");
            }

            if (resumeDTO.typeId <= 0)
            {
                throw new InvalidOperationException("Resume typeId must be a positive value.");
            }

            if (await ResumeNameExistsAsync(resumeDTO.Name, userId))
            {
                throw new InvalidOperationException("A Resume with the same name already exists for this user.");
            }
            var resume = new ResumeEntity
            {
                name = resumeDTO.Name,
                content = resumeDTO.content,
                is_public = resumeDTO.isPublic,
                created_at = DateTime.UtcNow,
                id_user_id = userId,
                image_url = resumeDTO.image_url,
                id_type_id = resumeDTO.typeId
            };

            if (resume.id_type_id == 0)
            {
                throw new InvalidOperationException("Resume typeid needs to be either 1 or 3");
            }

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
                Name = resume.name,
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
            resume.name = resumeDTO.Name;
            resume.content = resumeDTO.content;
            resume.is_public = resumeDTO.isPublic;
            resume.image_url = resumeDTO.image_url;

            await _dbContext.SaveChangesAsync();

            var response = "flashcard deleted successfully";
            return new ResumeResponse
            {
                Response =  response,
                ResumeId = resume.id_resume,
                Name = resume.name,
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

            var relatedMaterials = _dbContext.Materials.Where(m => m.id_resume_id == resumeId);
            _dbContext.Materials.RemoveRange(relatedMaterials);

            _dbContext.Resumes.Remove(resume);
            await _dbContext.SaveChangesAsync();

            var response = "Resume deleted successfully";
            return new ResumeResponse
            {
                Response = response,
                ResumeId = resume.id_resume,
                Name = resume.name,
                Content = resume.content,
                IsPublic = resume.is_public,
                CreatedAt = resume.created_at,
                UserId = resume.id_user_id,
                ImageUrl = resume.image_url,
                TypeId = resume.id_type_id,
            };
        }

        public async Task<bool> ResumeNameExistsAsync(string resumeName, int userId)
        {
            return await _dbContext.Resumes
              .AnyAsync(r => r.name == resumeName && r.id_user_id == userId);
        }
    }
}
