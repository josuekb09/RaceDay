using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDayApi.Data;
using RaceDayApi.DTOs;
using RaceDayApi.Models;

namespace RaceDayApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly RaceDayDbContext _context;

        public ResultsController(RaceDayDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CaptureResult([FromBody] CaptureResultDto dto)
        {
            var role = HttpContext.Session.GetString("UserRole");
            if (role != "Organiser")
                return StatusCode(StatusCodes.Status403Forbidden, new { message = "Only Organisers can capture race results." });

            var result = new Result
            {
                EnrolmentId = dto.EnrolmentId,
                FinishTime = dto.FinishTime,
                FinishingPosition = dto.FinishingPosition
            };

            _context.Results.Add(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }

        [HttpGet("enrolment/{enrolmentId}")]
        public async Task<IActionResult> GetResult(int enrolmentId)
        {
            var result = await _context.Results
                .Include(r => r.Enrolment)
                .FirstOrDefaultAsync(r => r.EnrolmentId == enrolmentId);

            if (result == null) return NotFound(new { message = "No result recorded yet." });

            return Ok(result);
        }
    }
}