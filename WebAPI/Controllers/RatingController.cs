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
    public class RatingController : ControllerBase
    {
        private readonly IRating _ratingService;
        private readonly StudiaDBContext _studiaDBContext;

        public RatingController(IRating ratingService, StudiaDBContext context)
        {
            _ratingService = ratingService;
            _studiaDBContext = context;
        }

        [HttpPost("create")]
        public async Task<ActionResult<RatingResponse>> CreateRating([FromBody] CreateRatingDTO ratingDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                return Unauthorized("User not authorized.");
            }

            try
            {
                var response = await _ratingService.CreateRatingAsync(ratingDTO, parsedUserId);
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating rating: {ex.Message}");
            }
        }

        [HttpGet("getByMaterial")]
        public async Task<ActionResult<List<RatingResponse>>> GetRatings([FromQuery] int? flashcardId, [FromQuery] int? examId, [FromQuery] int? resumeId, [FromQuery] int? noteId)
        {
            try
            {
                var ratings = await _ratingService.GetRatingsByResourceAsync(flashcardId, examId, resumeId, noteId);

                if (ratings == null || !ratings.Any())
                {
                    return NotFound("No ratings found for the specified resource.");
                }

                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving ratings: {ex.Message}");
            }
        }
        [HttpGet("get")]
        public async Task<ActionResult> GetRatings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var parsedUserId))
            {
                return Unauthorized("User not authorized.");
            }

            try
            {
                var ratings = await _ratingService.GetRatingsAsync(parsedUserId);

                if (ratings == null || !ratings.Any())
                {
                    return NotFound("No ratings found for the user.");
                }

                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving ratings: {ex.Message}");
            }
        }


    }
}
