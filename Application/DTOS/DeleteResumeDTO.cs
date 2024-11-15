using System.ComponentModel.DataAnnotations;

namespace Application.DTOS
{
    public class DeleteResumeDTO
    {
        [Required]
        public int ResumeId { get; set; }
    }
}
