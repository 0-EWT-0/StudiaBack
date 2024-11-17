

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
     public class RatingEntity
    {
        [Key]

        public int id_rating { get; set; }

        [Required]

        public int id_user_id { get; set; }

        public int ? id_flashcard_id { get; set; }

        public int ? id_exam_id { get; set; }

        public int ? id_resume_id { get; set; }

        public int ? id_notes_id { get; set; }

        [Required]
        [Range(1,5)]
        public int rating { get; set; }

        public DateTime created_at { get; set; }


        [ForeignKey("id_user_id")]

        public UserEntity User { get; set; } = null!;

        [ForeignKey("id_flashcard_id")]

        public FlashcardEntity ? Flashcard { get; set; } = null!;

        [ForeignKey("id_exam_id")]

        public ExamEntity ? Exam { get; set; } = null!;

        [ForeignKey("id_resume_id")]

        public ResumeEntity ? Resume { get; set; } = null!;

        [ForeignKey("id_notes_id")]

        public NoteEntity ? Note { get; set; } = null!;

    } 
}
