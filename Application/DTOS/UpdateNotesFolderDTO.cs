using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class UpdateNotesFolderDTO
    {
        public int FolderId { get; set; }
        public List<int> NoteIds { get; set; }
    }
}
