using System.ComponentModel.DataAnnotations;

namespace Application.DTOS
{
    public class CreateNoteDTO
    {
        [Required]

        public string content { get; set; } = string.Empty;

        public bool isPublic { get; set; } = false;
    }
}
