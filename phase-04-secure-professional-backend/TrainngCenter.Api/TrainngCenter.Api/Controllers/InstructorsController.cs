using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainngCenter.Api.DTOs.Instructors;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InstructorsController : ControllerBase
    {
        private readonly IInstructorService _instructorService;
        private readonly ICurrentUserService _currentUserService;

        public InstructorsController(IInstructorService instructorService , ICurrentUserService currentUserService)
        {
            _instructorService = instructorService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllInstructors()
        {
            var instructors = await _instructorService.GetAllAsync();

            return Ok(new
            {
                success = true,
                data = instructors
            });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetInstructorById(int id)
        {
            if (_currentUserService.Role == "Instructor" &&_currentUserService.InstructorId != id)
            {
                return Forbid();
            }

            var instructor = await _instructorService.GetInstructorById(id);

            if (instructor == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Instructor not found"
                });
            }

            return Ok(new
            {
                success = true,
                data = instructor
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateInstructor(CreateInstructorRequest request)
        {
            var result =
                await _instructorService.CreateAsync(request);

            if (!result.Success)
            {
                return Conflict(new
                {
                    success = false,
                    message = result.Message
                });
            }

            return CreatedAtAction(
                nameof(GetInstructorById),
                new { id = result.Data!.InstructorId },
                new
                {
                    success = true,
                    message = result.Message,
                    data = result.Data
                });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> UpdateInstructor(int id, UpdateInstructorRequest request)
        {
            if (_currentUserService.Role == "Instructor" && _currentUserService.InstructorId != id)
            {
                return Forbid();
            }

            var result =
                await _instructorService.UpdateAsync(id, request);

            if (!result.Success)
            {
                return Conflict(new
                {
                    success = false,
                    message = result.Message
                });
            }

            return Ok(new
            {
                success = true,
                message = result.Message
            });
        }

        [HttpGet("{id}/tracks")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetTracks(int id)
        {
            if (_currentUserService.Role == "Instructor" && _currentUserService.InstructorId != id)
            {
                return Forbid();
            }

            var result =
                await _instructorService.GetTracksAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Instructor not found"
                });
            }

            return Ok(new
            {
                success = true,
                data = result
            });
        }
    }
}