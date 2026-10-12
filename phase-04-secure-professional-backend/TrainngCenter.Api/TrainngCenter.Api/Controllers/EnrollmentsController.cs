using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Api.Services.Interfaces;
using TrainngCenter.Api.DTOs.Enrollments;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    [Authorize]
    public class EnrollmentsController : ControllerBase
    {
        private readonly IEnrollmentService _service;
        private readonly ICurrentUserService _currentUserService;

        public EnrollmentsController(
            IEnrollmentService service,
            ICurrentUserService currentUserService)
        {
            _service = service;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Instructor")]
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

            if (trackId.HasValue && trackId.Value <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Track ID must be greater than zero"
                });
            }

            if (studentId.HasValue && studentId.Value <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Student ID must be greater than zero"
                });
            }

            if (_currentUserService.Role == "Instructor")
            {
                if (!_currentUserService.InstructorId.HasValue)
                    return Forbid();

                if (!trackId.HasValue)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Instructor must specify a trackId"
                    });
                }

                var instructorId =
                    await _service.GetTrackInstructorIdAsync(trackId.Value);

                if (!instructorId.HasValue)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Training track was not found"
                    });
                }

                if (instructorId.Value !=
                    _currentUserService.InstructorId.Value)
                {
                    return Forbid();
                }
            }

            try
            {
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
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin,Instructor,Student")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Enrollment ID must be greater than zero"
                });
            }

            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Enrollment was not found"
                });
            }

            if (_currentUserService.Role == "Student")
            {
                if (!_currentUserService.StudentId.HasValue ||
                    result.StudentId !=
                    _currentUserService.StudentId.Value)
                {
                    return Forbid();
                }
            }

            if (_currentUserService.Role == "Instructor")
            {
                if (!_currentUserService.InstructorId.HasValue)
                    return Forbid();

                var instructorId =
                    await _service.GetTrackInstructorIdAsync(
                        result.TrainingTrackId);

                if (!instructorId.HasValue)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Training track was not found"
                    });
                }

                if (instructorId.Value !=
                    _currentUserService.InstructorId.Value)
                {
                    return Forbid();
                }
            }

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> Create(
            [FromBody] CreateEnrollmentRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Request body is required"
                });
            }

            if (_currentUserService.Role == "Student")
            {
                if (!_currentUserService.StudentId.HasValue)
                    return Forbid();

                if (request.StudentId !=
                    _currentUserService.StudentId.Value)
                {
                    return Forbid();
                }
            }

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            [FromBody] UpdateEnrollmentStatusRequest request)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Enrollment ID must be greater than zero"
                });
            }

            if (request == null ||
                string.IsNullOrWhiteSpace(request.Status))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Enrollment status is required"
                });
            }

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
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetStudentEnrollments(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Student ID must be greater than zero"
                });
            }

            if (_currentUserService.Role == "Student")
            {
                if (!_currentUserService.StudentId.HasValue ||
                    _currentUserService.StudentId.Value != id)
                {
                    return Forbid();
                }
            }

            var result =
                await _service.GetStudentEnrollmentsAsync(id);

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Enrollment ID must be greater than zero"
                });
            }

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
