using Application.Contracts;
using Application.DTOS;
using Application.DTOS.Responses;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResumeController : ControllerBase
    {
        private readonly IResumes _resumeService;
        private readonly StudiaDBContext _studiaDBContext;

        public ResumeController(IResumes resumeService, StudiaDBContext context)
        {
            _resumeService = resumeService;
            _studiaDBContext = context;
        }

        [HttpGet("get")]
        public async Task<ActionResult> GetResumes()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User unauthorized.");
            }

            try
            {
                var exams = await _studiaDBContext.Resumes
                    .Where(n => n.id_user_id == int.Parse(userId))
                    .ToListAsync();

                return Ok(exams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error getting exams: {ex.Message}");
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<ResumeResponse>> CreateResume([FromBody] CreateResumeDTO resumeDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Usuario no autorizado.");
            }

            try
            {
                var response = await _resumeService.CreateResumeAsync(resumeDTO, int.Parse(userId));
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{resumeId}")]
        public async Task<ActionResult<ResumeResponse>> DeleteResume(int resumeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Usuario no autorizado.");
            }

            try
            {
                var response = await _resumeService.DeleteResumeAsync(resumeId, int.Parse(userId));
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar el resumen: {ex.Message}");
            }
        }

        [HttpPut("update/{resumeId}")]
        public async Task<ActionResult<ResumeResponse>> UpdateResume(int resumeId, [FromBody] UpdateResumeDTO updateResumeDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Usuario no autorizado.");
            }
            try
            {
                var response = await _resumeService.UpdateResumeAsync(updateResumeDTO, int.Parse(userId), resumeId);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar el contenido del resumen: {ex.Message}");
            }
        }
    }
}
