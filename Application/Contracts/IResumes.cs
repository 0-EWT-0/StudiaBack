using Application.DTOS.Responses;
using Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IResumes
    {
        Task<ResumeResponse> CreateResumeAsync(CreateResumeDTO resumeDTO, int userId);

        Task<ResumeResponse> UpdateResumeAsync(UpdateResumeDTO resumeDTO, int userId, int resumeId);

        Task<ResumeResponse> DeleteResumeAsync(int resumeId, int userId);
    }
}
