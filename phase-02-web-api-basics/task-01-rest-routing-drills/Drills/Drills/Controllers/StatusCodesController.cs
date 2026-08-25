using Drills.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Drills.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusCodesController : ControllerBase
    {
        // Drill 14 - Status Code Practice
        // GET: api/statuscodes/100
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 100)
            {
                return Ok(new
                {
                    id = 100,
                    name = "Sample Note"
                });
            }

            return NotFound(new
            {
                message = "Note not found"
            });
        }

        // POST: api/statuscodes
        [HttpPost]
        public IActionResult Create([FromBody] CreateNoteRequest request)
        {
            var note = new Note
            {
                Id = 100,
                Title = request.Title,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow
            };

            return Created($"/api/statuscodes/{note.Id}", note);
        }

        // DELETE: api/statuscodes/100
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id != 100)
            {
                return NotFound(new
                {
                    message = "Note not found"
                });
            }

            return NoContent();
        }

        // POST: api/statuscodes/validate
        [HttpPost("validate")]
        public IActionResult Validate([FromBody] CreateNoteRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(new
                {
                    message = "Title is required"
                });
            }

            return Ok(new
            {
                message = "Request is valid"
            });
        }
    }
}