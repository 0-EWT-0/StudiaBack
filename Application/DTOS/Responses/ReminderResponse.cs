
namespace Application.DTOS.Responses
{
    public class ReminderResponse
    {
        public int ReminderId { get; set; }
        public int UserId { get; set; }
        public int? ExamId { get; set; }
        public int? FlashcardId { get; set; }
        public int? ResumeId { get; set; }
        public ExamResponse? Exam { get; set; }
        public FlashcardResponse? Flashcard { get; set; }
        public ResumeResponse? Resume { get; set; }
        public DateTime ReminderDate { get; set; }
        public string Response { get; set; } = string.Empty;
    }
}
