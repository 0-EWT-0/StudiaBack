using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses
{
    public class DeleteMultipleNotesResponse
    {
        public List<int> NoteIds { get; set; } = new List<int>(); public string Response { get; set; } = string.Empty;
    }
}
