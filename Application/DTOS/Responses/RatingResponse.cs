
using System.Security;

namespace Application.DTOS.Responses
{
    public class RatingResponse
    {
        public string Response { get; set; } = string.Empty;
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public int ? Rating { get; set; }
        public int ? NoteId { get; set; }
        public int ? FlaschardId { get; set; }
        public int ? ExamId { get; set; }
        public int ? ResumeId {  get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
