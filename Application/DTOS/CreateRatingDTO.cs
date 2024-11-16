

using System.ComponentModel.DataAnnotations;

namespace Application.DTOS
{
    public class CreateRatingDTO
    {
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        public int? FlashcardId { get; set; }
        public int? ExamId { get; set; }
        public int? ResumeId { get; set; }
        public int? NoteId { get; set; }

    }
}
