using Application.Contracts;
using Application.DTOS;
using Application.DTOS.Responses;
using Domain.Entities;
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
    public class ResponseController : ControllerBase
    {
        private readonly IResponse response;
        private readonly StudiaDBContext _studiaDBContext;

        public ResponseController(IResponse response, StudiaDBContext context)
        {
            this.response = response;
            _studiaDBContext = context;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ResponseResponse>> CreateResponse([FromBody] CreateResponseDTO responseDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User not authorized.");
            }

            try
            {
                var result = await response.CreateResponseAsync(responseDTO, int.Parse(userId));
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating response: {ex.Message}");
            }
        }

        [HttpGet("get")]

        public async Task<IActionResult> ListResponses()
        {
            List<ResponseEntity> responses;
            try
            {
                responses = await _studiaDBContext.Responses.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al obtener las respuestas: {ex.Message}");
            }
            return Ok(responses);
        }
    }
}
