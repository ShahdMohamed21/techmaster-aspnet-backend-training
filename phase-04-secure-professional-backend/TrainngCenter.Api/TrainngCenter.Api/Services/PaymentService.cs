
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<PaymentService> _logger;
        private readonly IAuditLogService _auditLogService;

        public PaymentService(ApplicationDbContext context,IMapper mapper,ILogger<PaymentService> logger,IAuditLogService auditLogService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _auditLogService = auditLogService;
        }

        public async Task<List<PaymentResponse>> GetAllAsync(DateTime? fromDate,DateTime? toDate,string? status)
        {
            var query = _context.Payments
                .AsNoTracking()
                .AsQueryable();

            if (fromDate.HasValue && toDate.HasValue
                && fromDate.Value > toDate.Value)
            {
                throw new ArgumentException(
                    "From date cannot be later than to date");
            }

            if (fromDate.HasValue)
            {
                query = query.Where(
                    p => p.PaymentDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(
                    p => p.PaymentDate <= toDate.Value);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                var allowedStatuses = new[]
                {
                    "Pending",
                    "Paid",
                    "Failed",
                    "Refunded"
                };

                var normalizedStatus = allowedStatuses.FirstOrDefault(
                    s => string.Equals(
                        s,
                        status.Trim(),
                        StringComparison.OrdinalIgnoreCase));

                if (normalizedStatus == null)
                {
                    throw new ArgumentException("Invalid payment status");
                }

                query = query.Where(
                    p => p.PaymentStatus == normalizedStatus);
            }

            var payments = await query
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();

            return _mapper.Map<List<PaymentResponse>>(payments);
        }

        public async Task<PaymentResponse?> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Payment ID must be greater than zero.");
            }

            var payment = await _context.Payments
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
            {
                return null;
            }

            return _mapper.Map<PaymentResponse>(payment);
        }

        public async Task<PaymentResponse> CreateAsync(CreatePaymentRequest request)
        {
            if (request == null)
            {
                throw new ArgumentException(
                    "Payment request is required");
            }

            if (request.Amount <= 0)
            {
                throw new ArgumentException(
                    "Payment amount must be greater than zero");
            }

            if (request.EnrollmentId <= 0)
            {
                throw new ArgumentException(
                    "A valid enrollment ID is required");
            }

            if (string.IsNullOrWhiteSpace(request.PaymentMethod))
            {
                throw new ArgumentException(
                    "Payment method is required");
            }

            if (string.IsNullOrWhiteSpace(request.ReferenceNumber))
            {
                throw new ArgumentException(
                    "Reference number is required");
            }

            var enrollmentExists = await _context.Enrollments
                .AnyAsync(e =>
                    e.EnrollmentId == request.EnrollmentId &&
                    !e.IsDeleted);

            if (!enrollmentExists)
            {
                throw new ArgumentException("Enrollment was not found");
            }

            var referenceNumber = request.ReferenceNumber.Trim();

            var referenceExists = await _context.Payments
                .AnyAsync(p =>
                    p.ReferenceNumber == referenceNumber);

            if (referenceExists)
            {
                throw new ArgumentException(
                    "Reference number already exists.");
            }

            var payment = _mapper.Map<Payment>(request);

            payment.ReferenceNumber = referenceNumber;
            payment.PaymentMethod = request.PaymentMethod.Trim();
            payment.PaymentDate = DateTime.UtcNow;
            payment.PaymentStatus = "Pending";

            _context.Payments.Add(payment);

            await _context.SaveChangesAsync();

            await _auditLogService.LogAsync(
                action: "PaymentCreated",
                entityName: "Payment",
                entityId: payment.PaymentId,
                description:
                    $"Payment {payment.PaymentId} was created with Pending status.",
                metadata: new
                {
                    payment.EnrollmentId,
                    payment.Amount,
                    payment.PaymentMethod,
                    Status = payment.PaymentStatus
                }
            );

            _logger.LogInformation(
                "Payment {PaymentId} created for Enrollment {EnrollmentId}.",
                payment.PaymentId,
                payment.EnrollmentId);

            return _mapper.Map<PaymentResponse>(payment);
        }

        public async Task<List<PaymentResponse>> GetEnrollmentPaymentsAsync( int enrollmentId)
        {
            if (enrollmentId <= 0)
            {
                throw new ArgumentException(
                    "Enrollment ID must be greater than zero.");
            }

            var enrollmentExists = await _context.Enrollments
                .AnyAsync(e =>
                    e.EnrollmentId == enrollmentId &&
                    !e.IsDeleted);

            if (!enrollmentExists)
            {
                throw new ArgumentException(
                    "Enrollment was not found.");
            }

            var payments = await _context.Payments
                .AsNoTracking()
                .Where(p => p.EnrollmentId == enrollmentId)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();

            return _mapper.Map<List<PaymentResponse>>(payments);
        }

        public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            if (id <= 0)
            {
                throw new ArgumentException(
                    "Payment ID must be greater than zero");
            }

            if (string.IsNullOrWhiteSpace(status))
            {
                throw new ArgumentException(
                    "Payment status is required");
            }

            var allowedStatuses = new[]
            {
                "Pending",
                "Paid",
                "Failed",
                "Refunded"
            };

            var normalizedStatus = allowedStatuses.FirstOrDefault(
                s => string.Equals(
                    s,
                    status.Trim(),
                    StringComparison.OrdinalIgnoreCase));

            if (normalizedStatus == null)
            {
                throw new ArgumentException(
                    "Invalid payment status. Allowed values: Pending, Paid, Failed, Refunded");
            }

            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
            {
                return false;
            }

            var oldStatus = payment.PaymentStatus;

            if (oldStatus == normalizedStatus)
            {
                return true;
            }

            payment.PaymentStatus = normalizedStatus;

            await _context.SaveChangesAsync();

            await _auditLogService.LogAsync(
                action: "PaymentStatusUpdated",
                entityName: "Payment",
                entityId: payment.PaymentId,
                description:
                    $"Payment status changed from {oldStatus} to {normalizedStatus}.",
                metadata: new
                {
                    OldStatus = oldStatus,
                    NewStatus = normalizedStatus,
                    payment.EnrollmentId
                }
            );

            _logger.LogInformation(
                "Payment {PaymentId} status changed from {OldStatus} to {NewStatus}.",
                payment.PaymentId,
                oldStatus,
                normalizedStatus);

            return true;
        }

        public async Task<int?> GetPaymentStudentIdAsync(int paymentId)
        {
            return await _context.Payments
                .AsNoTracking()
                .Where(p => p.PaymentId == paymentId)
                .Select(p => (int?)p.Enrollment.StudentId)
                .FirstOrDefaultAsync();
        }

        public async Task<int?> GetEnrollmentStudentIdAsync(int enrollmentId)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Where(e =>
                    e.EnrollmentId == enrollmentId &&
                    !e.IsDeleted)
                .Select(e => (int?)e.StudentId)
                .FirstOrDefaultAsync();
        }
    }
}
