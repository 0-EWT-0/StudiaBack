
using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOS
{
    public class UpdateExamDTO
    {
        [Required]
        public int ExamId {  get; set; }

        public string Content { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public ExamDifficulty Difficulty { get; set; }

        public bool IsPublic { get; set; } = false;
        

    }
}
