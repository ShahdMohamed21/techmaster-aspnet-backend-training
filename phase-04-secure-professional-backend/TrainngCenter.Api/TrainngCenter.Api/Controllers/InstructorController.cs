using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainngCenter.Api.DTOs.Instructor;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Controllers
{
    [ApiController]
    [Route("api/instructor")]
    [Authorize(Roles = "Instructor")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorService _service;
        private readonly ICurrentUserService _currentUser;

        public InstructorController(IInstructorService service,ICurrentUserService currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        private int? InstructorId => _currentUser.InstructorId;

        [HttpGet("my-tracks")]
        public async Task<IActionResult> GetMyTracks()
        {
            if (InstructorId == null)
                return Forbid();

            var tracks = await _service.GetMyTracksAsync(InstructorId.Value);

            return Ok(new
            {
                success = true,
                data = tracks
            });
        }

        [HttpGet("tracks/{id:int}/students")]
        public async Task<IActionResult> GetTrackStudents(int id)
        {
            if (InstructorId == null)
                return Forbid();

            var students = await _service.GetTrackStudentsAsync(InstructorId.Value, id);

            if (students == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Track not found or not assigned to you"
                });
            }

            return Ok(new
            {
                success = true,
                data = students
            });
        }

        [HttpPost("tracks/{id:int}/sessions")]
        public async Task<IActionResult> CreateSession(int id,[FromBody] CreateSessionRequest request)
        {
            if (InstructorId == null)
                return Forbid();

            try
            {
                var session = await _service.CreateSessionAsync(InstructorId.Value, id, request);

                return StatusCode(201, new
                {
                    success = true,
                    data = session
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPut("sessions/{id:int}")]
        public async Task<IActionResult> UpdateSession(int id,[FromBody] UpdateSessionRequest request)
        {
            if (InstructorId == null)
                return Forbid();

            try
            {
                var session = await _service.UpdateSessionAsync(InstructorId.Value, id, request);

                if (session == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Session not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = session
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpGet("tracks/{id:int}/progress")]
        public async Task<IActionResult> GetTrackProgress(int id)
        {
            if (InstructorId == null)
                return Forbid();

            try
            {
                var progress = await _service.GetTrackProgressAsync(InstructorId.Value, id);

                return Ok(new
                {
                    success = true,
                    data = progress
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}