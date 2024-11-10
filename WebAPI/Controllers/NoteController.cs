using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Contracts;
using Application.DTOS;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : Controller
    {
        private readonly INotes _noteRepository;
        private readonly StudiaDBContext _studiaDBContext;

        public NoteController(INotes noteRepository, StudiaDBContext context)
        {
            _noteRepository = noteRepository;
            _studiaDBContext = context;
        }

        [HttpGet("get")]
        public async Task<ActionResult> GetUserNotes(int folderId) 
        {
            try
            {
                var notes = await _studiaDBContext.Notes
                    .Where(n => n.id_folder_id == folderId)
                    .ToListAsync();

                return Ok(notes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las notas: {ex.Message}");
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<NoteResponse>> CreateNote(int folderId, [FromBody] CreateNoteDTO noteDTO)
        {
            try
            {
                var response = await _noteRepository.CreateNoteAsync(noteDTO, folderId);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
