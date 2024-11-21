using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class UpdateReminderDTO
    {
        [Required] 
        public int reminderId { get; set; }
        [Required] 
        public int userId { get; set; }
        public int? examId { get; set; } = null;
        public int? flashcardId { get; set; } = null;
        public int? resumeId { get; set; } = null;
        [Required] 
        public DateTime reminderDate { get; set; }
    }
}
