namespace Application.DTOS.Responses
{
    public class ResumeResponse
    {
        public string Response { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int ResumeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public required string Content { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TypeId { get; set; }
    }
}
