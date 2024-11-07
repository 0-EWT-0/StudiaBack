
using Application.DTOS;

namespace Application.Contracts
{
    public interface IFolders
    {
        Task<FolderResponse> CreateFolderAsync(CreateFolderDTO folderDTO, int user);
        Task<bool> FolderExistsAsync(string folderName, int userId);
    }
}
