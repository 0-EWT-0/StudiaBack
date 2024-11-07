namespace Application.DTOS
{
    public class FolderResponse
    {
        public int UserId { get; set; }
        public int FolderId { get; set; }
        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
