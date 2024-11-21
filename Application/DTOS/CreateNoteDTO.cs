using System.ComponentModel.DataAnnotations;

namespace Application.DTOS
{
    public class CreateNoteDTO
    {

        [Required]

        public string Name { get; set; } = string.Empty;

        [Required]

        public string content { get; set; } = string.Empty;

        public bool isPublic { get; set; } = false;
    }
}
