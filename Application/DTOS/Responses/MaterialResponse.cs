using Domain.Enums;

namespace Application.DTOS.Responses
{
    public class MaterialResponse
    {
        public int MaterialId { get; set; }
        public int UserId { get; set; }
        public ExamResponse? Exam { get; set; }
        public FlashcardResponse? Flashcard { get; set; }
        public ResumeResponse? Resume { get; set; }
    }

}
