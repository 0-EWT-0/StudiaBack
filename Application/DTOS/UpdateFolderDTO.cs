

namespace Application.DTOS
{
    public class UpdateFolderDTO
    {
        public int folderId { get; set; }

        public string newName { get; set; } = string.Empty;

        public bool isPublic { get; set; }
    }
}
