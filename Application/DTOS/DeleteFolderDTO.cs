
using System.ComponentModel.DataAnnotations;

namespace Application.DTOS
{
    public class DeleteFolderDTO
    {
        [Required]
        public int folderId { get; set; }
    }
}
