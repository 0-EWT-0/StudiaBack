// WebAPI/Controllers/FolderController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Application.Contracts;
using Application.DTOS;
using Infrastructure.Repo;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FolderController : ControllerBase
    {
        private readonly IFolders folder;

        public FolderController(IFolders folder)
        {
            this.folder = folder;
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
