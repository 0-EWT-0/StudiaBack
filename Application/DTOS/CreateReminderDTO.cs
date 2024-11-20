using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class CreateReminderDTO
    {
        [Required]
        public int userId { get; set; }
        public int? examId { get; set; }
        public int? flashcardId { get; set; }
        public int? resumeId { get; set; }
        [Required]
        public DateTime reminderDate{ get; set; }
    }
}