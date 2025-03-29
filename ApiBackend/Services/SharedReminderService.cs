using ApiBackend.Models;
using ApiBackend.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Services
{
    public class SharedReminderService
    {
        private readonly ReminderdbContext _context;
        public SharedReminderService(ReminderdbContext context)
        {
            _context = context;
        }

        //Get all shared reminders
        public async Task<List<SharedReminders>> GetSharedReminders()
        {
            return await _context.SharedReminders.ToListAsync();
        }

        //Get shared reminder by id
        public async Task<SharedReminders?> GetById(int id)
        {
            return await _context.SharedReminders.FirstOrDefaultAsync(sr => sr.Id == id);
        }

        //Get shared reminders by user id
        public async Task<List<SharedReminders>> GetByUserId(int userId)
        {
            return await _context.SharedReminders.Where(sr => sr.UserId == userId).ToListAsync();
        }

        //Get shared reminders by reminder id
        public async Task<List<SharedReminders>> GetByReminderId(int reminderId)
        {
            return await _context.SharedReminders.Where(sr => sr.ReminderId == reminderId).ToListAsync();
        }

        //Create a shared reminder
        public async Task<SharedReminders> CreateSharedReminder(SharedReminders sharedReminder)
        {
            _context.SharedReminders.Add(sharedReminder);
            await _context.SaveChangesAsync();
            return sharedReminder;
        }

        //Update a shared reminder
        public async Task<bool> UpdateSharedReminder(int id, SharedReminders updatedSharedReminder)
        {
            var sharedReminder = await _context.SharedReminders.FindAsync(id);
            if (sharedReminder == null) return false;

            sharedReminder.UserId = updatedSharedReminder.UserId;
            sharedReminder.ReminderId = updatedSharedReminder.ReminderId;

            await _context.SaveChangesAsync();
            return true;
        }

        //Delete a shared reminder
        public async Task<bool> DeleteSharedReminder(int id)
        {
            var sharedReminder = await _context.SharedReminders.FindAsync(id);
            if (sharedReminder == null) return false;

            _context.SharedReminders.Remove(sharedReminder);
            await _context.SaveChangesAsync();
            return true;
        }

        //Check if a reminder is already shared with a user
        public async Task<bool> IsReminderSharedWithUser(int reminderId, int userId)
        {
            return await _context.SharedReminders.AnyAsync(sr => sr.ReminderId == reminderId && sr.UserId == userId);
        }
    }
} 