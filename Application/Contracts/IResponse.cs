

using Application.DTOS;
using Application.DTOS.Responses;

namespace Application.Contracts
{
    public interface IResponse
    {
        Task<ResponseResponse> CreateResponseAsync (CreateResponseDTO responseDTO, int userId);
    }
}
