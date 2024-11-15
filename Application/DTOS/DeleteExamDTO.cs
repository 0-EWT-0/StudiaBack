
using System.ComponentModel.DataAnnotations;

namespace Application.DTOS
{
    public class DeleteExamDTO
    {
        [Required]
        public int ExamId { get; set; }
    }
}
