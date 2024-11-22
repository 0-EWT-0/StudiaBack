using Application.DTOS.Responses;
using Application.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface IReminders
    {
        Task<ReminderResponse> CreateReminderAsync(CreateReminderDTO reminderDTO, int userId); 
        Task<ReminderResponse> UpdateReminderAsync(UpdateReminderDTO reminderDTO, int userId); 
        Task<ReminderResponse> DeleteReminderAsync(int reminderId, int userId); 
        Task<List<ReminderResponse>> GetReminderAsync(int userId );
    }
}
