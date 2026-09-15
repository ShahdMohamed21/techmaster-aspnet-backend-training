using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Students;
using TrainingCenter.Api.Entities.DTOs.Tracks;

namespace TrainingCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrainingTracksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TrainingTracksController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}/students")]
        public async Task<IActionResult> GetStudents(int id)
        {
            var trackExists = await _context.TrainingTracks
                .AnyAsync(t => t.Id == id);

            if (!trackExists)
                return NotFound();

            var students = await _context.Enrollments
                .Where(e => e.TrainingTrackId == id)
                .Select(e => new StudentResponseDto
                {
                    Id = e.Student.Id,
                    FullName = e.Student.FullName,
                    Email = e.Student.Email
                })
                .ToListAsync();

            return Ok(students);
        }

        // projection
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrackDetails(int id)
        {
            var track = await _context.TrainingTracks
                .Where(t => t.Id == id)
                .Select(t => new TrackDetailsDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    InstructorName = t.Instructor.FullName
                })
                .FirstOrDefaultAsync();

            if (track == null)
                return NotFound();

            return Ok(track);
        }
    }
}