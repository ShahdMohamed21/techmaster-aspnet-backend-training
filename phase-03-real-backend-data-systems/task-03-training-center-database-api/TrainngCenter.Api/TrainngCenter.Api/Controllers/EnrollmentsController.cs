using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Api.Services.Interfaces;
using TrainngCenter.Api.DTOs.Enrollments;

namespace TrainngCenter.Api.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _service;

        public EnrollmentsController(IEnrollmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? status,
            [FromQuery] int? trackId,
            [FromQuery] int? studentId,
            [FromQuery] string? paymentStatus,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            if (pageNumber < 1 || pageSize < 1 || pageSize > 100)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Page number must be greater than 0 and page size must be between 1 and 100"
                });
            }

            var result = await _service.GetAllAsync(
                status,
                trackId,
                studentId,
                paymentStatus,
                pageNumber,
                pageSize);

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Enrollment was not found"
                });
            }

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromBody] CreateEnrollmentRequest request)
        {
            try
            {
                var result = await _service.CreateAsync(request);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = result.EnrollmentId },
                    new
                    {
                        success = true,
                        data = result
                    });
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

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            [FromBody] UpdateEnrollmentStatusRequest request)
        {
            try
            {
                var updated = await _service.UpdateStatusAsync(
                    id,
                    request.Status);

                if (!updated)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Enrollment was not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Enrollment status updated successfully"
                });
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

        [HttpGet("/api/students/{id:int}/enrollments")]
        public async Task<IActionResult> GetStudentEnrollments(int id)
        {
            var result = await _service.GetStudentEnrollmentsAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Student was not found"
                });
            }

            return Ok(new
            {
                success = true,
                data = result
            });
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Enrollment was not found"
                });
            }

            return Ok(new
            {
                success = true,
                message = "Enrollment deleted successfully"
            });
        }
    }
}
