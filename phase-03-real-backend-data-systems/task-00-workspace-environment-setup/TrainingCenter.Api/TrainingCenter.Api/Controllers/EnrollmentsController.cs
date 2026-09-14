using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.Entities.DTOs.Payments;

namespace TrainingCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EnrollmentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnrollmentsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}/payment-summary")]
        public async Task<IActionResult> GetPaymentSummary(int id)
        {
            var paymentSummary = await _context.PaymentSummaries
                .Where(p => p.EnrollmentId == id)
                .Select(p => new PaymentSummaryResponseDto
                {
                    Id = p.Id,
                    EnrollmentId = p.EnrollmentId,
                    TotalRequired = p.TotalRequired,
                    TotalPaid = p.TotalPaid,
                    RemainingAmount = p.TotalRequired - p.TotalPaid,
                    PaymentStatus = p.PaymentStatus.ToString()
                })
                .FirstOrDefaultAsync();

            if (paymentSummary == null)
                return NotFound();

            return Ok(paymentSummary);
        }
    }
}
