using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS
{
    public class DeleteReminderDTO
    {
        [Required]
        public int reminderId { get; set; }
    }
}
