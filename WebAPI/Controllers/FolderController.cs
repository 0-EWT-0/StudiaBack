// WebAPI/Controllers/FolderController.cs
using Application.Contracts;
using Application.DTOS;
using Application.DTOS.Responses;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FolderController : ControllerBase
    {
        private readonly IFolders folder;

        private readonly StudiaDBContext _studiaDBContext;


        public FolderController(IFolders folder, StudiaDBContext context)
        {
            this.folder = folder;

            _studiaDBContext = context;
        }

        [HttpGet("get")]
        public async Task<ActionResult> GetUserFolders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User unauthorized.");
            }

            try
            {
                // Llamar al repositorio para obtener los folders del usuario
                var folders = await folder.GetFoldersAsync(int.Parse(userId));

                // Si no hay folders, devolver un mensaje informativo
                if (folders == null || !folders.Any())
                {
                    return NotFound("No folders found for the user.");
                }

                // Retornar los folders
                return Ok(folders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving folders: {ex.Message}");
            }
        }



        [HttpPost("create")]
        public async Task<ActionResult<FolderResponse>> CreateFolder([FromBody] CreateFolderDTO folderDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User unauthorized.");
            }

            try
            {
                var response = await folder.CreateFolderAsync(folderDTO, int.Parse(userId));
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{folderId}")]
        public async Task<ActionResult<FolderResponse>> DeleteFolder(int folderId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User unauthorized");
            }

            try
            {
                var response = await folder.DeleteFolderAsync(folderId, int.Parse(userId));
                return Ok(response);
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
        public async Task<ActionResult<DeleteMultipleFoldersResponse>> DeleteMultipleFolders([FromBody] List<int> folderIds)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("Usuario no autorizado.");
            }

            try
            {
                var response = await folder.DeleteMultipleFoldersAsync(folderIds);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al eliminar las carpetas: {ex.Message}");
            }
        }


        [HttpPut("update/{folderId}")]
        public async Task<ActionResult<FolderResponse>> UpdateFolderName(int folderId, [FromBody] UpdateFolderDTO updateFolderDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User unauthorized");
            }

            try
            {
                //var updateFolderDTO = new UpdateFolderDTO { folderId = folderId, newName = newName, isPublic = isPublic };

                var response = await folder.UpdateFolderAsync(updateFolderDTO, int.Parse(userId));

                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating folder name: {ex.Message}");
            }
        }

    }
}
