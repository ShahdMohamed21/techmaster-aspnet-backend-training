using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Api.Entities;
using TrainngCenter.Api.DTOs.Instructors;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorsController : ControllerBase
    {
        

        private readonly IInstructorService _instructorService;
        public InstructorsController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllInstructors()
        {
            var instructors = await _instructorService.GetAllAsync();
            return Ok(new
            {
                success = true,
                Data= instructors
            });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstructorById(int id)
        {
            var instructor = await _instructorService.GetInstructorById(id);
            if(instructor == null)
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
                Data = instructor
            });

        }
        [HttpPost]
        public async Task<IActionResult> CraeteInstructor(CreateInstructorRequest request)
        {
            var result = await _instructorService.CreateAsync(request);
            if(!result.Success)
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
        public async Task<IActionResult> UpdateInstructor(int id,UpdateInstructorRequest request)
        {
            var result = await _instructorService.UpdateAsync(id,request);
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
        public async Task<IActionResult> GetTracks(int id)
        {
            var result = await _instructorService.GetTracksAsync(id);

            if (result == null)
                return NotFound(new
                {
                    success = false,
                    message = "Instructor not found."
                });

            return Ok(new
            {
                success = true,
                data = result
            });
        }
    }
}
