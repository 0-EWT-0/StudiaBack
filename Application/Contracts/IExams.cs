

using Application.DTOS;
using Application.DTOS.Responses;

namespace Application.Contracts
{
    public interface IExams
    {
        Task<ExamResponse> CreateExamAsync(CreateExamDTO examDTO, int userId);

        Task<ExamResponse> UpdateExamAsync(UpdateExamDTO examDTO, int userId);

        Task<ExamResponse> DeleteExamAsync(int examId, int userId); 

    }
}
