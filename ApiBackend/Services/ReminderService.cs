using ApiBackend.Models;
using ApiBackend.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Services
{
    public class ReminderService
    {
        private readonly ReminderdbContext _context;
        public ReminderService(ReminderdbContext context)
        {
            _context = context;
        }

        //Get all reminders
        public async Task<List<Reminders>> GetReminders()
        {
            return await _context.Reminders.ToListAsync();
        }

        //Get reminder by id
        public async Task<Reminders?> GetById(int id)
        {
            return await _context.Reminders.FirstOrDefaultAsync(r => r.Id == id);
        }

        //Get reminders by user id
        public async Task<List<Reminders>> GetByUserId(int userId)
        {
            return await _context.Reminders.Where(r => r.UserId == userId).ToListAsync();
        }

        //Create a reminder
        public async Task<Reminders> CreateReminder(Reminders reminder)
        {
            _context.Reminders.Add(reminder);
            await _context.SaveChangesAsync();
            return reminder;
        }

        //Update a reminder
        public async Task<bool> UpdateReminder(int id, Reminders updatedReminder)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null) return false;

            reminder.Title = updatedReminder.Title;
            reminder.ReminderType = updatedReminder.ReminderType;
            reminder.Description = updatedReminder.Description;
            reminder.Status = updatedReminder.Status;
            reminder.UserId = updatedReminder.UserId;

            await _context.SaveChangesAsync();
            return true;
        }

        //Delete a reminder
        public async Task<bool> DeleteReminder(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);
            if (reminder == null) return false;

            _context.Reminders.Remove(reminder);
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 