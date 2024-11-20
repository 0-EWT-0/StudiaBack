using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses
{
    public class ReminderResponse
    {
        public int ReminderId { get; set; }
        public int UserId { get; set; }
        public int? ExamId { get; set; }
        public int? FlashcardId { get; set; }
        public int? ResumeId { get; set; }
        public DateTime ReminderDate { get; set; }
        public string Response { get; set; } = string.Empty;
    }
}
