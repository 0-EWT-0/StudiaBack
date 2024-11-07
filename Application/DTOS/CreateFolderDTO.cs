
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace Application.DTOS
{
    public class CreateFolderDTO
    {
        [Required]
        public string name {  get; set; } = string.Empty;

        public bool isPublic { get; set; } = false;
    }
}
