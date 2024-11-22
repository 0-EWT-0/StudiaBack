using Application.Contracts;
using Application.DTOS;
using Application.DTOS.Responses;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class ReminderRepo : IReminders
    {
        private readonly StudiaDBContext _dbContext;

        public ReminderRepo(StudiaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ReminderResponse> CreateReminderAsync(CreateReminderDTO reminderDTO, int userId)
        {
            var reminder = new ReminderEntity
            {
                id_user_id = userId,
                id_exam_id = reminderDTO.examId != 0 ? reminderDTO.examId : null,
                id_flashcard_id = reminderDTO.flashcardId != 0 ? reminderDTO.flashcardId : null,
                id_resume_id = reminderDTO.resumeId != 0 ? reminderDTO.resumeId : null,
                reminder_date = reminderDTO.reminderDate
            };

            _dbContext.Reminders.Add(reminder);
            await _dbContext.SaveChangesAsync();

            return new ReminderResponse
            {
                ReminderId = reminder.id_reminder,
                UserId = reminder.id_user_id,
                ExamId = reminder.id_exam_id,
                FlashcardId = reminder.id_flashcard_id,
                ResumeId = reminder.id_resume_id,
                ReminderDate = reminder.reminder_date,
                Response = "Reminder created successfully"
            };
        }

        public async Task<ReminderResponse> UpdateReminderAsync(UpdateReminderDTO reminderDTO, int userId)
        {
            var reminder = await _dbContext.Reminders.FirstOrDefaultAsync(r => r.id_reminder == reminderDTO.reminderId && r.id_user_id == userId);

            if (reminder == null)
            {
                throw new InvalidOperationException("Reminder not found or does not belong to the user");
            }

            reminder.id_exam_id = reminderDTO.examId != 0 ? reminderDTO.examId : null;
            reminder.id_flashcard_id = reminderDTO.flashcardId != 0 ? reminderDTO.flashcardId : null;
            reminder.id_resume_id = reminderDTO.resumeId != 0 ? reminderDTO.resumeId : null;
            reminder.reminder_date = reminderDTO.reminderDate;

            await _dbContext.SaveChangesAsync();

            return new ReminderResponse
            {
                ReminderId = reminder.id_reminder,
                UserId = reminder.id_user_id,
                ExamId = reminder.id_exam_id,
                FlashcardId = reminder.id_flashcard_id,
                ResumeId = reminder.id_resume_id,
                ReminderDate = reminder.reminder_date,
                Response = "Reminder updated successfully"
            };
        }

        public async Task<ReminderResponse> DeleteReminderAsync(int reminderId, int userId)
        {
            var reminder = await _dbContext.Reminders.FirstOrDefaultAsync(r => r.id_reminder == reminderId && r.id_user_id == userId);

            if (reminder == null)
            {
                throw new InvalidOperationException("Reminder not found or does not belong to the user");
            }

            _dbContext.Reminders.Remove(reminder);
            await _dbContext.SaveChangesAsync();

            return new ReminderResponse
            {
                ReminderId = reminder.id_reminder,
                UserId = reminder.id_user_id,
                ExamId = reminder.id_exam_id,
                FlashcardId = reminder.id_flashcard_id,
                ResumeId = reminder.id_resume_id,
                ReminderDate = reminder.reminder_date,
                Response = "Reminder deleted successfully"
            };
        }

        public async Task<List<ReminderResponse>> GetReminderAsync(int userId)
        {
            return await _dbContext.Reminders
       .Where(r => r.id_user_id == userId)
       .Include(r => r.Exam)
       .Include(r => r.Flashcard)
       .Include(r => r.Resume)
       .Select(r => new ReminderResponse
       {
           ReminderId = r.id_reminder,
           UserId = r.id_user_id,
           Exam = r.Exam != null ? new ExamResponse
           {
               ExamId = r.Exam.id_exam,
               Name = r.Exam.name,
               Content = r.Exam.content,
               ImageUrl = r.Exam.image_url,
               Difficulty = r.Exam.difficulty,
               IsPublic = r.Exam.is_public,
               CreatedAt = r.Exam.created_at
           } : null,
           Flashcard = r.Flashcard != null ? new FlashcardResponse
           {
               FlashcardId = r.Flashcard.id_flashcard,
               Name = r.Flashcard.name,
               Content = r.Flashcard.content,
               ImageUrl = r.Flashcard.image_url,
               IsPublic = r.Flashcard.is_public,
               CreatedAt = r.Flashcard.created_at
           } : null,
           Resume = r.Resume != null ? new ResumeResponse
           {
               ResumeId = r.Resume.id_resume,
               Content = r.Resume.content,
               IsPublic = r.Resume.is_public,
               CreatedAt = r.Resume.created_at
           } : null,
           ReminderDate = r.reminder_date,
           Response = "Reminder retrieved successfully"
       })
       .ToListAsync();
        }
    }
}
