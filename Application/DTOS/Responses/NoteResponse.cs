namespace Application.DTOS.Responses
{
    public class NoteResponse
    {
        public string Response { get; set; } = string.Empty;
        public int FolderId { get; set; }
        public int NoteId { get; set; }
        public string Content { get; set; } = null!;
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
