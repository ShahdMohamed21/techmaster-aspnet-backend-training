using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Api.DTOs.Students;
using TrainingCenter.Api.Services.Interfaces;

namespace TrainingCenter.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll( string? search, bool? isActive,int page = 1, int pageSize = 10)
        {
            var result = await _service.GetAllAsync(search, isActive, page,pageSize);
            return Ok(new
            {
                success = true,
                data = result
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Student not found"
                });
            }

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create( CreateStudentRequest request)
        {
            var result = await _service.CreateAsync(request);

            if (!result.Success)
            {
                return Conflict(new
                {
                    success = false,
                    message = result.Message
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Data!.StudentId },
                new
                {
                    success = true,
                    message = result.Message,
                    data = result.Data
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update( int id, UpdateStudentRequest request)
        {
            var result = await _service.UpdateAsync(id, request);

            if (!result.Success)
            {
                if (result.Message == "Student not found")
                    return NotFound(new
                    {
                        success = false,
                        message = result.Message
                    });

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.Success)
            {
                return NotFound(new
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
    }
}