using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Api.DTOs.Students;
using TrainingCenter.Api.Services.Interfaces;
using TrainngCenter.Api.DTOs.Enrollments;
using TrainngCenter.Api.DTOs.Students;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainingCenter.Api.Controllers
{
    [ApiController]
    [Route("api/students")]
    [Authorize]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEnrollmentService _enrollmentService;

        public StudentsController(IStudentService service, ICurrentUserService currentUserService, IEnrollmentService enrollmentService)
        {
            _service = service;
            _currentUserService = currentUserService;
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(string? search,bool? isActive,int page = 1,int pageSize = 10)
        {
            var result = await _service.GetAllAsync(search, isActive, page, pageSize);

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> GetById(int id)
        {
            if (_currentUserService.Role == "Student" && _currentUserService.StudentId != id)
            {
                return Forbid();
            }

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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateStudentRequest request)
        {
            var result = await _service.CreateAsync(request);

            if (!result.Success)
            {
                return BadRequest(new
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
        [Authorize(Roles = "Admin,Student")]
        public async Task<IActionResult> Update(int id,UpdateStudentRequest request)
        {
            if (_currentUserService.Role == "Student" && _currentUserService.StudentId != id)
            {
                return Forbid();
            }

            var result = await _service.UpdateAsync(id, request);

            if (!result.Success)
            {
                if (result.Message == "Student not found")
                {
                    return NotFound(new
                    {
                        success = false,
                        message = result.Message
                    });
                }

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
        [Authorize(Roles = "Admin")]
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

        [HttpGet("me")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetMyProfile()
        {
            if (_currentUserService.StudentId == null)
                return Forbid();

            var student = await _service.GetMyProfileAsync(_currentUserService.StudentId.Value);

            if (student == null)
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
                data = student
            });
        }

        [HttpGet("my-enrollments")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetMyEnrollments()
        {
            if (_currentUserService.StudentId == null)
                return Forbid();

            var enrollments = await _service.GetMyEnrollmentsAsync(_currentUserService.StudentId.Value);

            return Ok(new
            {
                success = true,
                data = enrollments
            });
        }

        [HttpGet("my-payments")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetMyPayments()
        {
            if (_currentUserService.StudentId == null)
                return Forbid();

            var payments = await _service.GetMyPaymentsAsync( _currentUserService.StudentId.Value);

            return Ok(new
            {
                success = true,
                data = payments
            });
        }

        [HttpPost("enrollment-requests")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> CreateEnrollmentRequest([FromBody] StudentEnrollmentRequest request)
        {
            if (_currentUserService.StudentId == null)
                return Forbid();

            var enrollmentRequest = new CreateEnrollmentRequest
            {
                StudentId = _currentUserService.StudentId.Value,
                TrainingTrackId = request.TrainingTrackId
            };

            try
            {
                var result = await HttpContext.RequestServices
                    .GetRequiredService<IEnrollmentService>()
                    .CreateAsync(enrollmentRequest);

                return StatusCode(201, new
                {
                    success = true,
                    message = "Enrollment request submitted successfully",
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

        [HttpPut("me")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> UpdateMyProfile(
            [FromBody] UpdateMyProfileRequest request)
        {
            if (_currentUserService.StudentId == null)
                return Forbid();

            var result = await _service.UpdateMyProfileAsync(_currentUserService.StudentId.Value, request);

            if (!result.Success)
            {
                if (result.Message == "Student not found or inactive")
                    return NotFound(new { success = false, message = result.Message });

                return Conflict(new { success = false, message = result.Message });
            }

            return Ok(new
            {
                success = true,
                message = result.Message
            });
        }





    }
}