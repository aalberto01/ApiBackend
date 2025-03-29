using ApiBackend.Services;
using ApiBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedReminderController : ControllerBase
    {
        private readonly SharedReminderService _sharedReminderService;
        public SharedReminderController(SharedReminderService sharedReminderService)
        {
            _sharedReminderService = sharedReminderService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SharedReminders>>> GetSharedReminders()
        {
            var sharedReminders = await _sharedReminderService.GetSharedReminders();
            return Ok(sharedReminders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SharedReminders>> GetSharedReminderById(int id)
        {
            var sharedReminder = await _sharedReminderService.GetById(id);
            if (sharedReminder == null) return NotFound("Recordatorio compartido no encontrado");

            return Ok(sharedReminder);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<SharedReminders>>> GetSharedRemindersByUserId(int userId)
        {
            var sharedReminders = await _sharedReminderService.GetByUserId(userId);
            return Ok(sharedReminders);
        }

        [HttpGet("reminder/{reminderId}")]
        public async Task<ActionResult<List<SharedReminders>>> GetSharedRemindersByReminderId(int reminderId)
        {
            var sharedReminders = await _sharedReminderService.GetByReminderId(reminderId);
            return Ok(sharedReminders);
        }

        [HttpPost]
        public async Task<ActionResult> CreateSharedReminder([FromBody] SharedReminders sharedReminder)
        {
            if (sharedReminder == null)
            {
                return BadRequest("Datos del recordatorio compartido vienen vacios");
            }

            if (!sharedReminder.ReminderId.HasValue || !sharedReminder.UserId.HasValue)
            {
                return BadRequest("El ID del recordatorio y del usuario son requeridos");
            }

            var isAlreadyShared = await _sharedReminderService.IsReminderSharedWithUser(sharedReminder.ReminderId.Value, sharedReminder.UserId.Value);
            if (isAlreadyShared)
            {
                return BadRequest("El recordatorio ya está compartido con este usuario");
            }

            var newSharedReminder = await _sharedReminderService.CreateSharedReminder(sharedReminder);
            return Ok(newSharedReminder);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSharedReminder(int id, [FromBody] SharedReminders updatedSharedReminder)
        {
            if (updatedSharedReminder == null)
            {
                return BadRequest("Datos del recordatorio compartido vienen vacios");
            }

            if (!updatedSharedReminder.ReminderId.HasValue || !updatedSharedReminder.UserId.HasValue)
            {
                return BadRequest("El ID del recordatorio y del usuario son requeridos");
            }

            var response = await _sharedReminderService.UpdateSharedReminder(id, updatedSharedReminder);

            if (response == false)
            {
                return NotFound("Recordatorio compartido no encontrado en base de datos");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSharedReminder(int id)
        {
            var response = await _sharedReminderService.DeleteSharedReminder(id);
            if (response == false)
            {
                return NotFound("Recordatorio compartido no encontrado en base de datos");
            }
            return NoContent();
        }
    }
} 