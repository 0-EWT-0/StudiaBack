using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Contracts;
using Application.DTOS;
using Application.DTOS.Responses;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReminderController : ControllerBase
    {
        private readonly IReminders _reminderService;

        public ReminderController(IReminders reminderService)
        {
            _reminderService = reminderService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ReminderResponse>> CreateReminder([FromBody] CreateReminderDTO reminderDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Usuario no autorizado.");
            }

            try
            {
                var response = await _reminderService.CreateReminderAsync(reminderDTO, int.Parse(userId));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el recordatorio: {ex.Message}");
            }
        }

        [HttpPut("update/{reminderId}")]
        public async Task<ActionResult<ReminderResponse>> UpdateReminder(int reminderId, [FromBody] UpdateReminderDTO reminderDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Usuario no autorizado.");
            }

            try
            {
                if (reminderId != reminderDTO.reminderId)
                {
                    return BadRequest("El ID proporcionado no coincide con el ID del recordatorio.");
                }

                var response = await _reminderService.UpdateReminderAsync(reminderDTO, int.Parse(userId));
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el recordatorio: {ex.Message}");
            }
        }

        [HttpDelete("delete/{reminderId}")]
        public async Task<ActionResult<ReminderResponse>> DeleteReminder(int reminderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Usuario no autorizado.");
            }

            try
            {
                var response = await _reminderService.DeleteReminderAsync(reminderId, int.Parse(userId));
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el recordatorio: {ex.Message}");
            }
        }

        [HttpGet("exists/{reminderId}")]
        public async Task<ActionResult<bool>> ReminderExists(int reminderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Usuario no autorizado.");
            }

            try
            {
                var exists = await _reminderService.ReminderExistsAsync(reminderId, int.Parse(userId));
                return Ok(exists);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al verificar la existencia del recordatorio: {ex.Message}");
            }
        }
    }
}
