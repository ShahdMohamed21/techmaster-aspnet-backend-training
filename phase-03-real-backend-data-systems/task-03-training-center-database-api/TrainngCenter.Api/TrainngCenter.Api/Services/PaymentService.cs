using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.Entities;
using TrainngCenter.Api.DTOs.Payments;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Services
{

    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PaymentService( ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PaymentResponse>> GetAllAsync(DateTime? fromDate, DateTime? toDate, string? status)
        {
            var query = _context.Payments
                .AsNoTracking()
                .AsQueryable();

            if (fromDate.HasValue)
            {
                query = query.Where(p => p.PaymentDate >= fromDate.Value);
            }


            if (toDate.HasValue)
            {
                query = query.Where(p => p.PaymentDate <= toDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(p => p.PaymentStatus == status);
            }

            var payments = await query
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();

            return _mapper.Map<List<PaymentResponse>>(payments);
        }

        public async Task<PaymentResponse?> GetByIdAsync(int id)
        {
            var payment = await _context.Payments.AsNoTracking()
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
            {
                return null;
            }

            return _mapper.Map<PaymentResponse>(payment);
        }

        public async Task<PaymentResponse> CreateAsync(CreatePaymentRequest request)
        {
            if (request.Amount <= 0)
            {
                throw new ArgumentException("Payment amount must be greater than zero");
            }

            if (string.IsNullOrWhiteSpace(request.PaymentMethod))
            {
                throw new ArgumentException("Payment method is required");
            }

            if (string.IsNullOrWhiteSpace(request.ReferenceNumber))
            {
                throw new ArgumentException("Reference number is required");
            }

            var enrollmentExists = await _context.Enrollments
                .AnyAsync(e => e.EnrollmentId == request.EnrollmentId);

            if (!enrollmentExists)
            {
                throw new ArgumentException("Enrollment was not found");
            }

            var referenceExists = await _context.Payments
                .AnyAsync(p =>
                    p.ReferenceNumber == request.ReferenceNumber);

            if (referenceExists)
            {
                throw new ArgumentException(
                    "Reference number already exists");
            }

            var payment = _mapper.Map<Payment>(request);

            payment.PaymentDate = DateTime.UtcNow;
            payment.PaymentStatus = "Pending";

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return _mapper.Map<PaymentResponse>(payment);
        }

        public async Task<List<PaymentResponse>> GetEnrollmentPaymentsAsync(int enrollmentId)
        {
            var enrollmentExists = await _context.Enrollments
                .AnyAsync(e => e.EnrollmentId == enrollmentId);

            if (!enrollmentExists)
            {
                throw new ArgumentException("Enrollment was not found");
            }

            var payments = await _context.Payments
                .AsNoTracking()
                .Where(p => p.EnrollmentId == enrollmentId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();

            return _mapper.Map<List<PaymentResponse>>(payments);
        }

        public async Task<bool> UpdateStatusAsync( int id,string status)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
                return false;

            var allowedStatuses = new[]
            {
                "Pending",
                "Paid",
                "Failed",
                "Refunded"
            };

            if (!allowedStatuses.Contains(status))
                throw new ArgumentException(
                    "Invalid payment status. Allowed values: Pending, Paid, Failed, Refunded");

            payment.PaymentStatus = status;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}

