using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses
{
    public class FlashcardResponse
    {
        public string Response { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int FlashcardId { get; set; }
        public required string Content { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsPublic { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
