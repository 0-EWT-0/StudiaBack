// WebAPI/Controllers/FolderController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Contracts;
using Application.DTOS;
using Infrastructure.Repo;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult> getUserFolders()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User unauthorized.");
            }

            List<FolderEntity> folders;

            try
            {
                folders = await _studiaDBContext.Folders
                    .Where(f => f.id_user_id == int.Parse(userId))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las carpetas: {ex.Message}");
            }

            return Ok(folders);
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
    }
}
