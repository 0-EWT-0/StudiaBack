using Application.Contracts;
using Application.DTOS.Responses;
using Application.DTOS;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FlashcardController : ControllerBase
    {
        private readonly IFlashcards _flashcardService;
        private readonly StudiaDBContext _studiaDBContext;

        public FlashcardController(IFlashcards flashcardService, StudiaDBContext context)
        {
            _flashcardService = flashcardService;
            _studiaDBContext = context;
        }

        [HttpGet("get")]
        public async Task<ActionResult> GetFlashcards()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User unauthorized.");
            }

            try
            {
                var exams = await _studiaDBContext.Flashcards
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
        public async Task<ActionResult<FlashcardResponse>> CreateFlashcard([FromBody] CreateFlashcardDTO flashcardDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Usuario no autorizado.");
            }

            try
            {
                var response = await _flashcardService.CreateFlashcardAsync(flashcardDTO, int.Parse(userId));
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{flashcardId}")]
        public async Task<ActionResult<FlashcardResponse>> DeleteFlashcard(int flashcardId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Usuario no autorizado.");
            }

            try
            {
                var response = await _flashcardService.DeleteFlashcardAsync(flashcardId, int.Parse(userId));
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar la flashcard: {ex.Message}");
            }
        }

        [HttpPut("update/{flashcardId}")] 
        public async Task<ActionResult<FlashcardResponse>> UpdateFlashcard(int flashcardId, [FromBody] UpdateFlashcardDTO updateFlashcardDTO) 
        { 
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            if (userId == null) 
            { 
                return Unauthorized("Usuario no autorizado."); 
            }
            
            try 
            { 
                var response = await _flashcardService.UpdateFlashcardAsync(updateFlashcardDTO, int.Parse(userId), flashcardId); 
                return Ok(response); 
            } 
            catch (InvalidOperationException ex) 
            { 
                return NotFound(ex.Message); 
            } catch (Exception ex) 
            { 
                return StatusCode(500, $"Error al actualizar el contenido de la flashcard: {ex.Message}"); 
            } 
        }
    }
}

