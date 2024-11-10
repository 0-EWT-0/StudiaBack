
namespace Application.DTOS
{
    public class NoteResponse
    {
        public int FolderId { get; set; }

        public int NoteId { get; set; }
        public string Content { get; set; }
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
