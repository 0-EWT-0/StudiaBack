namespace Application.DTOS.Responses
{
    public class FolderResponse
    {
        public string Response { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int FolderId { get; set; }
        public required string Name { get; set; }
        public List<NoteResponse>? Notes { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
