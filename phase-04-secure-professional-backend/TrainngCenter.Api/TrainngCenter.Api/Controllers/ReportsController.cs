using Microsoft.AspNetCore.Mvc;
using TrainngCenter.Api.Services;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Controllers
{

    [ApiController]
    [Route("api/reports")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("dashboard-summary")]
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
        public async Task<IActionResult> GetUnpaidEnrollments()
        {
            var result = await _service.GetUnpaidEnrollmentsAsync();

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        [HttpGet("track-capacity")]
        public async Task<IActionResult> GetTrackCapacity()
        {
            var result = await _service.GetTrackCapacityAsync();

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        [HttpGet("revenue-summary")]
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
        public async Task<IActionResult> GetTopTracks(int top = 5)
        {
            if (top < 1 || top > 50)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Top must be between 1 and 50"
                });
            }

            var result = await _service.GetTopTracksAsync(top);

            return Ok(new
            {
                success = true,
                data = result
            });
        }
        [HttpGet("instructor-workload")]
        public async Task<IActionResult> GetInstructorWorkload()
        {
            var result = await _service.GetInstructorWorkloadAsync();

            return Ok(new
            {
                success = true,
                data = result
            });
        }
        [HttpGet("students-without-payments")]
        public async Task<IActionResult> GetStudentsWithoutPayments()
        {
            var result = await _service.GetStudentsWithoutPaymentsAsync();

            return Ok(new
            {
                success = true,
                data = result
            });
        }
    }
}
