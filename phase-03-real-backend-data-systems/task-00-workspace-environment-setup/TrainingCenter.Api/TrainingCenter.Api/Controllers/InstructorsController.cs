using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.TrainingTracks;

namespace TrainingCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InstructorsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}/tracks")]
        public async Task<IActionResult> GetTracks(int id)
        {
            var instructorExists = await _context.Instructors
                .AnyAsync(i => i.Id == id);

            if (!instructorExists)
                return NotFound();

            var tracks = await _context.TrainingTracks
                .Where(t => t.InstructorId == id)
                .Select(t => new TrainingTrackResponseDto
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();

            return Ok(tracks);
        }
    }
}
