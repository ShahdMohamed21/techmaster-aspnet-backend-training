using Microsoft.AspNetCore.Mvc;
using TrainingCenter.Api.Entities;
using TrainngCenter.Api.DTOs.Payments;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentsController(IPaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] DateTime? fromDate,[FromQuery] DateTime? toDate, [FromQuery] string? status)
        {
            if (fromDate.HasValue &&
                toDate.HasValue &&
                fromDate > toDate)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "fromDate must be earlier than or equal to toDate"
                });
            }

            var result = await _service.GetAllAsync(
                fromDate,
                toDate,
                status);

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
                    message = "Payment was not found"
                });
            }

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentRequest request)
        {
            try
            {
                var result = await _service.CreateAsync(request);

                return CreatedAtAction(
                    nameof(GetById),
                    new { id = result.PaymentId },
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

        [HttpGet("~/api/enrollments/{id:int}/payments")]
        public async Task<IActionResult> GetEnrollmentPayments(int id)
        {
            try
            {
                var result = await _service.GetEnrollmentPaymentsAsync(id);

                return Ok(new
                {
                    success = true,
                    data = result
                });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }

        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            [FromBody] UpdatePaymentStatusRequest request)
        {
            try
            {
                var updated = await _service.UpdateStatusAsync(
                    id,
                    request.PaymentStatus);

                if (!updated)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "Payment was not found"
                    });
                }

                return Ok(new
                {
                    success = true,
                    message = "Payment status updated successfully"
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


