using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.TrainingTracks;

namespace TrainingCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StudentsController(AppDbContext context)
        {
            _context = context;
            
        }

        [HttpGet("{id}/tracks")]
        public async Task<IActionResult> GetTracks(int id)
        {
            var studentExists = await _context.Students
                .AnyAsync(s => s.Id == id);

            if (!studentExists)
                return NotFound();

            var tracks = await _context.Enrollments
                .Where(e => e.StudentId == id)
                .Select(e => new TrainingTrackResponseDto
                {
                    Id = e.TrainingTrack.Id,
                    Name = e.TrainingTrack.Name
                })
                .ToListAsync();

            return Ok(tracks);
        }
    }
}
