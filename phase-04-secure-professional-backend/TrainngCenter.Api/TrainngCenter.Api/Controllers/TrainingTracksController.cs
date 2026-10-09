using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainngCenter.Api.DTOs.Tracks;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Controllers
{
    [ApiController]
    [Route("api/tracks")]
    [Authorize]
    public class TrainingTracksController : ControllerBase
    {
        private readonly ITrackService _service;
        private readonly ICurrentUserService _currentUserService;

        public TrainingTracksController(
            ITrackService service,
            ICurrentUserService currentUserService)
        {
            _service = service;
            _currentUserService = currentUserService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,Instructor,Student")]
        public async Task<IActionResult> GetAll(string? keyword,string? level,string? status,int? instructorId)
        {
            var role = _currentUserService.Role;

            if (role == "Instructor")
            {
                if (!_currentUserService.InstructorId.HasValue)
                    return Forbid();

                instructorId = _currentUserService.InstructorId.Value;
            }

            if (role == "Student")
            {
                status = "Open";
            }

            var result = await _service.GetAllAsync(
                keyword,
                level,
                status,
                instructorId);

            if (role == "Student")
            {
                result = result
                    .Where(t => t.EnrolledStudents < t.Capacity)
                    .ToList();
            }

            return Ok(new
            {
                success = true,
                data = result
            });
        }
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Instructor,Student")]
        public async Task<IActionResult> GetById(int id)
        {
            var track = await _service.GetByIdAsync(id);

            if (track == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Training track not found"
                });
            }

            if (_currentUserService.Role == "Instructor")
            {
                if (!_currentUserService.InstructorId.HasValue ||
                    track.InstructorId != _currentUserService.InstructorId.Value)
                {
                    return Forbid();
                }
            }

            if (_currentUserService.Role == "Student")
            {
                if (track.Status != "Open" ||
                    track.EnrolledStudents >= track.Capacity)
                {
                    return Forbid();
                }
            }

            return Ok(new
            {
                success = true,
                data = track
            });
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateTrackRequest request)
        {
            var result = await _service.CreateAsync(request);

            if (!result.success)
            {
                return BadRequest(new
                {
                    success = false,
                    message = result.message
                });
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Data!.TrainingTrackId },
                new
                {
                    success = true,
                    message = result.message,
                    data = result.Data
                });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> Update(
            int id,
            UpdateTrackRequest request)
        {
            if (_currentUserService.Role == "Instructor")
            {
                if (!_currentUserService.InstructorId.HasValue)
                    return Forbid();

                var track = await _service.GetByIdAsync(id);

                if (track == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Training track not found"
                    });
                }

                if (track.InstructorId != _currentUserService.InstructorId.Value)
                {
                    return Forbid();
                }
            }

            var result = await _service.UpdateAsync(id, request);

            if (!result.success)
            {
                if (result.message == "Training track not found")
                {
                    return NotFound(new
                    {
                        success = false,
                        message = result.message
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = result.message
                });
            }

            return Ok(new
            {
                success = true,
                message = result.message
            });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.success)
            {
                if (result.message == "Training track not found")
                {
                    return NotFound(new
                    {
                        success = false,
                        message = result.message
                    });
                }

                return BadRequest(new
                {
                    success = false,
                    message = result.message
                });
            }

            return Ok(new
            {
                success = true,
                message = result.message
            });
        }

        [HttpGet("{id}/students")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetStudents(int id)
        {
            if (_currentUserService.Role == "Instructor")
            {
                if (!_currentUserService.InstructorId.HasValue)
                    return Forbid();

                var track = await _service.GetByIdAsync(id);

                if (track == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Training track not found"
                    });
                }

                if (track.InstructorId != _currentUserService.InstructorId.Value)
                {
                    return Forbid();
                }
            }

            var result = await _service.GetStudentsAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Training track not found"
                });
            }

            return Ok(new
            {
                success = true,
                data = result
            });
        }
      [HttpPut("{id:int}/assign-instructor")]
      [Authorize(Roles = "Admin")]
       public async Task<IActionResult> AssignInstructor(int id, [FromBody] AssignInstructorRequest request)
        {
            try
            {
                var assigned = await _service.AssignInstructorAsync(
                    id,
                    request.InstructorId);

                if (!assigned)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Training track was not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Instructor assigned to training track successfully"
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

    }
}