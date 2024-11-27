using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly StudiaDBContext _context;

        public TestController(StudiaDBContext context)
        {
            _context = context;
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection()
        {
            // Prueba de consulta a la base de datos
            List<UserEntity> users;
            try
            {
                users = await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al conectarse a la base de datos: {ex.Message}");
            }

            // Devuelve los usuarios como resultado para verificar la conexión
            return Ok(users);
        }
    }
}
