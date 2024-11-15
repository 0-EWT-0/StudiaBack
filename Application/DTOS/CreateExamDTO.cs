

using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOS
{
    public class CreateExamDTO
    {
        [Required]

        public string Content { get; set; } = string.Empty;

        public string Image_url { get; set; } = string.Empty;

        public bool IsPublic { get; set; } = false;

        [Required]

        public ExamDifficulty Difficulty { get; set; }

        [Required]

        public int TypeId {  get; set; } 

    }
}
