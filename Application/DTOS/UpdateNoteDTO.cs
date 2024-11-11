using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class UpdateNoteDTO
    {
        public int noteId {  get; set; }
        
        public string newContent {  get; set; } = string.Empty;

        public bool isPublic { get; set; } 

    }
}
