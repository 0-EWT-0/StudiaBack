
using Application.DTOS.Responses;

namespace Application.Contracts
{
    public interface IMaterials
    {
        Task<List<MaterialResponse>> GetMaterialsByUserIdAsync(int userId);
    }
}
