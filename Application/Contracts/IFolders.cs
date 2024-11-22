
using Application.DTOS;
using Application.DTOS.Responses;

namespace Application.Contracts
{
    public interface IFolders
    {
        Task<List<FolderResponse>> GetFoldersAsync(int userId);
        Task<FolderResponse> CreateFolderAsync(CreateFolderDTO folderDTO, int user);
        Task<FolderResponse> UpdateFolderAsync(UpdateFolderDTO folderDTO, int userId);
        Task<FolderResponse> DeleteFolderAsync(int folderId, int userId);
        Task<bool> FolderExistsAsync(string folderName, int userId);
    }
}
