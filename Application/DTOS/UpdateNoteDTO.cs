using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class UpdateNoteDTO
    {
        [Required]
        public int folderId { get; set; }  // Añadido para actualizar el folderId
        public int noteId {  get; set; }

        public string Name { get; set; } = string.Empty;

        public string newContent {  get; set; } = string.Empty;

        public bool isPublic { get; set; } 

    }
}
