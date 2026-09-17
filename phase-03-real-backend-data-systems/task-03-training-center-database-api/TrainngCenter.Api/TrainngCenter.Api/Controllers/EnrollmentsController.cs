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
        public async Task<IActionResult> GetAll([FromQuery] string? status,[FromQuery] int? trackId,[FromQuery] int? studentId,[FromQuery] string? paymentStatus)
        {
            var result = await _service.GetAllAsync(
                status, trackId, studentId, paymentStatus);

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
                return NotFound(new
                {
                    success = false,
                    message = "Enrollment was not found"
                });

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEnrollmentRequest request)
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

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, UpdateEnrollmentStatusRequest request)
        {
            try
            {
                var updated = await _service.UpdateStatusAsync(id, request.Status);
                if (!updated)
                    return NotFound(new
                    {
                        success = false,
                        message = "Enrollment was not found"
                    });

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

        [HttpGet("/api/students/{id}/enrollments")]
        public async Task<IActionResult> GetStudentEnrollments(int id)
        {
            var result = await _service.GetStudentEnrollmentsAsync(id);

            if(result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Student Not Found"
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

