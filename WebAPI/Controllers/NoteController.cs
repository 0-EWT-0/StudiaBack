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
    public class NoteController : Controller
    {
        private readonly INotes note;
        private readonly StudiaDBContext _studiaDBContext;

        public NoteController(INotes note, StudiaDBContext context)
        {
            this.note = note;
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
                var response = await note.CreateNoteAsync(noteDTO, folderId);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{noteId}")]

        public async Task<ActionResult<NoteResponse>> UpdateNoteContent([FromBody] UpdateNoteDTO updateNoteDto)
        {
            try
            {

                //var updateNoteDto = new UpdateNoteDTO { noteId = noteId, newContent = newContent, isPublic = isPublic};

                var response = await note.UpdateNoteAsync(updateNoteDto);

                return Ok(response);

            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating note content: {ex.Message}");
            }
        }


        // Nuevo método para actualizar las notas en una carpeta
        [HttpPut("update-notes-folder")]
        public async Task<IActionResult> UpdateNotesFolder([FromBody] UpdateNotesFolderDTO updateNotesFolderDto)
        {
            try
            {
                var result = await note.UpdateNotesFolderAsync(updateNotesFolderDto);
                return Ok(new { message = "Notas actualizadas correctamente." });
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al actualizar las notas: {ex.Message}");
            }
        }



        [HttpDelete("delete/{noteId}")]

        public async Task<ActionResult<NoteResponse>> DeleteNote(int noteId)
        {
            try
            {
                var result = await note.DeleteNoteAsync(noteId);

                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting folder and notes: {ex.Message}");
            }
        }


        [HttpDelete("delete/multiple")]
        public async Task<ActionResult<DeleteMultipleNotesResponse>> DeleteMultipleNotes([FromBody] List<int> noteIds)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Usuario no autorizado.");
            }

            try
            {
                var response = await note.DeleteMultipleNotesAsync(noteIds);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar las notas: {ex.Message}");
            }
        }

    }
}
