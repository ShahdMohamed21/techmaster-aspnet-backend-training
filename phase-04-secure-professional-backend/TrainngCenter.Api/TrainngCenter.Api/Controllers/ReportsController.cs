using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Controllers
{
    [ApiController]
    [Route("api/reports")]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;
        private readonly ICurrentUserService _currentUserService;

        public ReportsController(
            IReportService service,
            ICurrentUserService currentUserService)
        {
            _service = service;
            _currentUserService = currentUserService;
        }



        [HttpGet("dashboard-summary")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDashboardSummary()
        {
            var result = await _service.GetDashboardSummaryAsync();

            return Ok(new
            {
                success = true,
                data = result
            });
        }


        [HttpGet("unpaid-enrollments")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetUnpaidEnrollments()
        {
            int? instructorId = null;

            if (_currentUserService.Role == "Instructor")
            {
                if (!_currentUserService.InstructorId.HasValue)
                    return Forbid();

                instructorId = _currentUserService.InstructorId.Value;
            }

            var result = await _service
                .GetUnpaidEnrollmentsAsync(instructorId);

            return Ok(new
            {
                success = true,
                data = result
            });
        }



        [HttpGet("track-capacity")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetTrackCapacity()
        {
            int? instructorId = null;

            if (_currentUserService.Role == "Instructor")
            {
                if (!_currentUserService.InstructorId.HasValue)
                    return Forbid();

                instructorId = _currentUserService.InstructorId.Value;
            }

            var result = await _service
                .GetTrackCapacityAsync(instructorId);

            return Ok(new
            {
                success = true,
                data = result
            });
        }



        [HttpGet("revenue-summary")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRevenueSummary()
        {
            var result = await _service.GetRevenueSummaryAsync();

            return Ok(new
            {
                success = true,
                data = result
            });
        }



        [HttpGet("revenue-by-track")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetRevenueByTrack()
        {
            var result = await _service.GetRevenueByTrackAsync();

            return Ok(new
            {
                success = true,
                data = result
            });
        }



        [HttpGet("top-tracks")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetTopTracks(
            [FromQuery] int top = 5)
        {
            if (top < 1 || top > 50)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Top must be between 1 and 50"
                });
            }

            int? instructorId = null;

            if (_currentUserService.Role == "Instructor")
            {
                if (!_currentUserService.InstructorId.HasValue)
                    return Forbid();

                instructorId = _currentUserService.InstructorId.Value;
            }

            var result = await _service
                .GetTopTracksAsync(top, instructorId);

            return Ok(new
            {
                success = true,
                data = result
            });
        }



        [HttpGet("instructor-workload")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetInstructorWorkload()
        {
            int? instructorId = null;

            if (_currentUserService.Role == "Instructor")
            {
                if (!_currentUserService.InstructorId.HasValue)
                    return Forbid();

                instructorId = _currentUserService.InstructorId.Value;
            }

            var result = await _service
                .GetInstructorWorkloadAsync(instructorId);

            return Ok(new
            {
                success = true,
                data = result
            });
        }

 

        [HttpGet("students-without-payments")]
        [Authorize(Roles = "Admin,Instructor")]
        public async Task<IActionResult> GetStudentsWithoutPayments()
        {
            int? instructorId = null;

            if (_currentUserService.Role == "Instructor")
            {
                if (!_currentUserService.InstructorId.HasValue)
                    return Forbid();

                instructorId = _currentUserService.InstructorId.Value;
            }

            var result = await _service
                .GetStudentsWithoutPaymentsAsync(instructorId);

            return Ok(new
            {
                success = true,
                data = result
            });
        }
    }
}