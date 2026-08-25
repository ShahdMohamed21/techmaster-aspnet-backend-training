using Drills.DTOs;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Drills.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        // Drill 06 - Create Note Endpoint
        private static readonly List<Note> Notes = new();
        private static int nextId = 1;
        [HttpPost]
        public IActionResult CreateNote([FromBody] CreateNoteRequest request)
        {
            var note = new Note
            {
                Id = nextId++,
                Title = request.Title,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow
            };

            Notes.Add(note);

            return StatusCode(201, note);  // response body
        }

        //Drill 07 - Get Notes List
       [HttpGet]
        public IActionResult GetNotes()
        {
            return Ok(Notes);     // كنت عملالها كومنت عشان فيه 2 get
        }
      
        //Drill 08 - Get Note By Id
        [HttpGet("{Id}")]
        public IActionResult GetNoteById(int Id)
        {
            var Note = Notes.FirstOrDefault(x => x.Id == Id);

            if (Note == null)
            {
                return NotFound(new
                {
                    message = "Note not found"
                });
            }

            return Ok(Note);
        }

        //Drill 09 - Update Note Endpoint
        [HttpPut("{Id}")]
        public IActionResult UpdateById(int Id, [FromBody] UpdateNoteRequest updateNoteRequest)
        {
            var Existnote = Notes.FirstOrDefault(x => x.Id == Id);
            if (Existnote == null)
            {
                return NotFound(new
                {
                    message = "Note not found"
                });
            }
            if (string.IsNullOrWhiteSpace(updateNoteRequest.Title) || string.IsNullOrWhiteSpace(updateNoteRequest.Content)) {
                return BadRequest(new
                {
                    Error = "Title And Content Should Not B e Null"
                });
            }

            Existnote.Title = updateNoteRequest.Title;
            Existnote.Content = updateNoteRequest.Content;

            return Ok(Existnote);
                
        }
        //Drill 10 - Delete Note Endpoint
        [HttpDelete("{Id}")]
        public IActionResult DeleteById(int Id)
        {
            var Existnote = Notes.FirstOrDefault(x => x.Id == Id);
            if (Existnote == null)
            {
                return NotFound(new
                {
                    message = "Note not found"
                });
            }
            Notes.Remove(Existnote);

            return NoContent();

        }
        //Drill 11 - Search Notes
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string Keyword){
            if (string.IsNullOrWhiteSpace(Keyword))
            {
                return BadRequest(new
                {
                    message = "Keyword is required"
                });
            }
            var Result=Notes.Where(x=> (x.Title.Contains(Keyword,StringComparison.OrdinalIgnoreCase)||x.Content.Contains(Keyword,StringComparison.OrdinalIgnoreCase))).ToList();
            return Ok(Result);
        }
        //Drill 12 - Pagination Demo
        [HttpGet]
        public IActionResult GetNotes( [FromQuery] int pageNumber,[FromQuery] int pageSize)
        {
            if (pageNumber <= 0)
            {
                return BadRequest(new
                {
                    message = "Page number must be greater than 0"
                });
            }
            if (pageSize < 1 || pageSize > 50)
            {
                return BadRequest(new
                {
                    message = "Page size must be between 1 and 50"
                });
            }
            var totalCount = Notes.Count;
            var skip = (pageNumber - 1) * pageSize;

            var items = Notes.Skip(skip).Take(pageSize).ToList();

            return Ok(new
            {
                items = items,
                pageNumber = pageNumber,
                pageSize = pageSize,
                totalCount = totalCount
            });
        }

    }
}
