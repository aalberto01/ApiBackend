using ApiBackend.Services;
using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReminderController : ControllerBase
    {
        private readonly ReminderService _reminderService;
        public ReminderController(ReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Reminders>>> GetReminders()
        {
            var reminders = await _reminderService.GetReminders();
            return Ok(reminders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reminders>> GetReminderById(int id)
        {
            var reminder = await _reminderService.GetById(id);
            if (reminder == null) return NotFound("Recordatorio no encontrado");

            return Ok(reminder);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Reminders>>> GetRemindersByUserId(int userId)
        {
            var reminders = await _reminderService.GetByUserId(userId);
            return Ok(reminders);
        }

        [HttpPost]
        public async Task<ActionResult> CreateReminder([FromBody] Reminders reminder)
        {
            if (reminder == null)
            {
                return BadRequest("Datos del recordatorio vienen vacios");
            }
            var newReminder = await _reminderService.CreateReminder(reminder);
            return Ok(newReminder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReminder(int id, [FromBody] Reminders updatedReminder)
        {
            if (updatedReminder == null)
            {
                return BadRequest("Datos del recordatorio vienen vacios");
            }

            var response = await _reminderService.UpdateReminder(id, updatedReminder);

            if (response == false)
            {
                return NotFound("Recordatorio no encontrado en base de datos");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReminder(int id)
        {
            var response = await _reminderService.DeleteReminder(id);
            if (response == false)
            {
                return NotFound("Recordatorio no encontrado en base de datos");
            }
            return NoContent();
        }
    }
} 