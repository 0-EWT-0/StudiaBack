
using Application.Contracts;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterials material;

        private readonly StudiaDBContext _studiaDBContext;


        public MaterialController(IMaterials material, StudiaDBContext context)
        {
            this.material = material;

            _studiaDBContext = context;
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetMaterials()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized("User not authorized.");
            }

            try
            {
                var materials = await material.GetMaterialsByUserIdAsync(int.Parse(userId));
                return Ok(materials);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving materials: {ex.Message}");
            }
        }
    }
}
