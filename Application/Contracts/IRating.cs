

using Application.DTOS;
using Application.DTOS.Responses;

namespace Application.Contracts
{
    public interface IRating
    {
        Task<RatingResponse> CreateRatingAsync(CreateRatingDTO ratingDTO, int userId);

        Task<List<RatingResponse>> GetRatingsByResourceAsync(int? flashcardId, int? examId, int? resumeId, int? noteId);
    }
}
