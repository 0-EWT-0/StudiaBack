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
    public class ExamController : ControllerBase
    {
        private readonly IExams exam;

        private readonly StudiaDBContext _studiaDBContext;


        public ExamController(IExams exam, StudiaDBContext context)
        {
            this.exam = exam;

            _studiaDBContext = context;
        }


        [HttpGet("get")]
        public async Task<ActionResult> GetExams()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User unauthorized.");
            }

            try
            {
                var exams = await _studiaDBContext.Exams
                    .Where(n => n.id_user_id == int.Parse(userId))
                    .Include(e => e.Type)
                    .ToListAsync();

                return Ok(exams);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error getting exams: {ex.Message}");
            }
        }

        [HttpPost("create")]
        public async Task<ActionResult<ExamResponse>> CreateExamAsync([FromBody] CreateExamDTO examDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User unauthorized.");
            }

            try
            {
                var response = await exam.CreateExamAsync(examDTO, int.Parse(userId));
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error creating exam: {ex.Message}");
            }
        }


        [HttpPut("update/{examId}")]
        public async Task<ActionResult<ExamResponse>> UpdateExam(int examId, [FromBody] UpdateExamDTO examDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User unauthorized.");
            }

            try
            {
                examDTO.ExamId = examId;
                var response = await exam.UpdateExamAsync(examDTO, int.Parse(userId));
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating exam: {ex.Message}");
            }
        }


        [HttpDelete("delete/{examId}")]
        public async Task<ActionResult<ExamResponse>> DeleteExam(int examId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User unauthorized.");
            }

            try
            {
                var result = await exam.DeleteExamAsync(examId, int.Parse(userId));
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error deleting exam: {ex.Message}");
            }
        }


    }



}
