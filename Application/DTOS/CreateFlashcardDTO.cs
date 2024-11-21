

using System.ComponentModel.DataAnnotations;

namespace Application.DTOS
{
    public class CreateFlashcardDTO
    {

        [Required]

        public string Name { get; set; } = string.Empty;

        public string content { get; set; } = string.Empty;

        public bool isPublic { get; set; } = false;

        public string image_url { get; set; } = string.Empty;

        [Required]
        public int typeId { get; set; }
    }
}
