

using Domain.Enums;

namespace Application.DTOS.Responses
{
    public class ExamResponse
    {
        public string Response {  get; set; } = string.Empty;

        public int ExamId { get; set; }

        public int UserId { get; set; }

        public int TypeId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public string ImageUrl {  get; set; } = string.Empty;
        public bool IsPublic { get; set; }

        public DateTime CreatedAt { get; set; } 
        public ExamDifficulty Difficulty { get; set; }

    }
}
