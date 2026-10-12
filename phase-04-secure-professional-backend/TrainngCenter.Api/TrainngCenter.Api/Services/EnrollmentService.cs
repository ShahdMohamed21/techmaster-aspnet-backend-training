using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Services.Interfaces;
using TrainngCenter.Api.DTOs.Enrollments;
using TrainngCenter.Api.DTOs.Students;

namespace TrainngCenter.Api.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EnrollmentService> _logger;

        public EnrollmentService(ApplicationDbContext context,ILogger<EnrollmentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        private static bool IsOccupyingSeat(Enrollment enrollment)
        {
            return !enrollment.IsDeleted &&
                   (enrollment.Status == "Active" ||
                    enrollment.Status == "Pending");
        }

        public async Task<PagedEnrollmentResponse> GetAllAsync(string? status,int? trackId,int? studentId,string? paymentStatus,int pageNumber,int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0 || pageSize > 100)
            {
                throw new ArgumentException(
                    "Page number must be greater than zero and page size must be between 1 and 100");
            }

            var query = _context.Enrollments
                .AsNoTracking()
                .Where(e =>
                    !e.IsDeleted &&
                    !e.Student.IsDeleted &&
                    !e.TrainingTrack.IsDeleted);

            if (!string.IsNullOrWhiteSpace(status))
            {
                var normalizedStatus = status.Trim();

                query = query.Where(e =>
                    e.Status == normalizedStatus);
            }

            if (trackId.HasValue)
            {
                if (trackId.Value <= 0)
                    throw new ArgumentException(
                        "Track ID must be greater than zero.");

                query = query.Where(e =>
                    e.TrainingTrackId == trackId.Value);
            }

            if (studentId.HasValue)
            {
                if (studentId.Value <= 0)
                    throw new ArgumentException(
                        "Student ID must be greater than zero.");

                query = query.Where(e =>
                    e.StudentId == studentId.Value);
            }

            if (!string.IsNullOrWhiteSpace(paymentStatus))
            {
                var normalizedPaymentStatus = paymentStatus.Trim();

                query = query.Where(e =>
                    e.Payments.Any(p =>
                        p.PaymentStatus == normalizedPaymentStatus));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(e => e.EnrollmentId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EnrollmentListItemResponse
                {
                    EnrollmentId = e.EnrollmentId,
                    StudentId = e.StudentId,
                    StudentName = e.Student.FullName,
                    TrainingTrackId = e.TrainingTrackId,
                    TrainingTrackTitle = e.TrainingTrack.Title,
                    Status = e.Status,
                    ProgressPercentage = e.ProgressPercentage
                })
                .ToListAsync();

            return new PagedEnrollmentResponse
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount
            };
        }

        public async Task<EnrollmentDetailsResponse?> GetByIdAsync(int id)
        {
            if (id <= 0)
                return null;

            var enrollment = await _context.Enrollments
                .AsNoTracking()
                .Where(e =>
                    !e.IsDeleted &&
                    !e.Student.IsDeleted &&
                    !e.TrainingTrack.IsDeleted &&
                    e.EnrollmentId == id)
                .Select(e => new EnrollmentDetailsResponse
                {
                    EnrollmentId = e.EnrollmentId,

                    StudentId = e.StudentId,
                    StudentName = e.Student.FullName,

                    TrainingTrackId = e.TrainingTrackId,
                    TrainingTrackTitle = e.TrainingTrack.Title,

                    EnrollmentDate = e.EnrollmentDate,
                    Status = e.Status,
                    ProgressPercentage = e.ProgressPercentage,
                    FinalResult = e.FinalResult,

                    Payments = e.Payments
                        .Select(p =>
                            new TrainngCenter.Api.DTOs.Payments.PaymentResponse
                            {
                                PaymentId = p.PaymentId,
                                EnrollmentId = p.EnrollmentId,
                                Amount = p.Amount,
                                PaymentMethod = p.PaymentMethod,
                                PaymentDate = p.PaymentDate,
                                PaymentStatus = p.PaymentStatus,
                                ReferenceNumber = p.ReferenceNumber,
                                Notes = p.Notes
                            })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return enrollment;
        }

        public async Task<EnrollmentDetailsResponse> CreateAsync(CreateEnrollmentRequest request)
        {
            if (request == null)
                throw new ArgumentException("Request is required.");

            if (request.StudentId <= 0)
                throw new ArgumentException(
                    "Student ID must be greater than zero.");

            if (request.TrainingTrackId <= 0)
                throw new ArgumentException(
                    "Training track ID must be greater than zero.");

            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s =>
                    s.StudentId == request.StudentId &&
                    s.IsActive &&
                    !s.IsDeleted);

            if (student == null)
            {
                _logger.LogWarning(
                    "Enrollment creation failed: student {StudentId} was not found or is inactive",
                    request.StudentId);

                throw new ArgumentException(
                    "Student does not exist, is inactive, or has been deleted.");
            }

            var track = await _context.TrainingTracks
                .AsNoTracking()
                .FirstOrDefaultAsync(t =>
                    t.TrainingTrackId == request.TrainingTrackId &&
                    !t.IsDeleted);

            if (track == null)
            {
                _logger.LogWarning(
                    "Enrollment creation failed: track {TrackId} was not found",
                    request.TrainingTrackId);

                throw new ArgumentException(
                    "Training track was not found.");
            }

            if (track.Status != "Open")
            {
                throw new ArgumentException(
                    "Enrollment is allowed only for open tracks.");
            }

            var alreadyEnrolled = await _context.Enrollments
                .AnyAsync(e =>
                    e.StudentId == request.StudentId &&
                    e.TrainingTrackId == request.TrainingTrackId &&
                    !e.IsDeleted &&
                    e.Status != "Cancelled");

            if (alreadyEnrolled)
            {
                _logger.LogWarning(
                    "Enrollment creation rejected because an existing enrollment was found for student {StudentId} and track {TrackId}",
                    request.StudentId,
                    request.TrainingTrackId);

                throw new ArgumentException(
                    "This student is already enrolled in this track.");
            }

            var enrolledCount = await _context.Enrollments
                .CountAsync(e =>
                    e.TrainingTrackId == request.TrainingTrackId &&
                    !e.IsDeleted &&
                    (e.Status == "Active" ||
                     e.Status == "Pending"));

            if (enrolledCount >= track.Capacity)
            {
                _logger.LogWarning(
                    "Enrollment creation rejected because track {TrackId} has reached capacity",
                    request.TrainingTrackId);

                throw new ArgumentException(
                    "The training track has reached its capacity.");
            }

            var enrollment = new Enrollment
            {
                StudentId = request.StudentId,
                TrainingTrackId = request.TrainingTrackId,
                EnrollmentDate = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
                Status = "Pending",
                ProgressPercentage = 0,
                IsDeleted = false
            };

            _context.Enrollments.Add(enrollment);

            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Enrollment {EnrollmentId} created successfully for track {TrackId}",
                enrollment.EnrollmentId,
                enrollment.TrainingTrackId);

            var result = await GetByIdAsync(enrollment.EnrollmentId);

            if (result == null)
            {
                throw new InvalidOperationException(
                    "Enrollment was created but could not be retrieved.");
            }

            return result;
        }

        public async Task<List<EnrollmentListItemResponse>?>
            GetStudentEnrollmentsAsync(int studentId)
        {
            if (studentId <= 0)
                return null;

            var studentExists = await _context.Students
                .AsNoTracking()
                .AnyAsync(s =>
                    s.StudentId == studentId &&
                    !s.IsDeleted);

            if (!studentExists)
                return null;

            var enrollments = await _context.Enrollments
                .AsNoTracking()
                .Where(e =>
                    e.StudentId == studentId &&
                    !e.IsDeleted &&
                    !e.TrainingTrack.IsDeleted)
                .OrderBy(e => e.EnrollmentId)
                .Select(e => new EnrollmentListItemResponse
                {
                    EnrollmentId = e.EnrollmentId,
                    StudentId = e.StudentId,
                    StudentName = e.Student.FullName,
                    TrainingTrackId = e.TrainingTrackId,
                    TrainingTrackTitle = e.TrainingTrack.Title,
                    Status = e.Status,
                    ProgressPercentage = e.ProgressPercentage
                })
                .ToListAsync();

            return enrollments;
        }

        public async Task<List<TrackStudentResponse>> GetTrackStudentsAsync(int trackId)
        {
            if (trackId <= 0)
                throw new ArgumentException(
                    "Track ID must be greater than zero.");

            var students = await _context.Enrollments
                .AsNoTracking()
                .Where(e =>
                    e.TrainingTrackId == trackId &&
                    !e.IsDeleted &&
                    !e.Student.IsDeleted)
                .Select(e => new TrackStudentResponse
                {
                    StudentId = e.Student.StudentId,
                    StudentName = e.Student.FullName,
                    Email = e.Student.Email,
                    EnrollmentId = e.EnrollmentId,
                    EnrollmentStatus = e.Status,
                    ProgressPercentage = e.ProgressPercentage
                })
                .ToListAsync();

            return students;
        }

        public async Task<bool> UpdateStatusAsync(int id,string status)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "Enrollment ID must be greater than zero.");

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException(
                    "Enrollment status is required.");

            var normalizedStatus = status.Trim();

            if (normalizedStatus != "Completed" &&
                normalizedStatus != "Cancelled")
            {
                throw new ArgumentException(
                    "Status must be Completed or Cancelled.");
            }

            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e =>
                    e.EnrollmentId == id &&
                    !e.IsDeleted);

            if (enrollment == null)
            {
                _logger.LogWarning(
                    "Enrollment status update failed: enrollment {EnrollmentId} was not found",
                    id);

                return false;
            }

            var oldStatus = enrollment.Status;

            enrollment.Status = normalizedStatus;
            enrollment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Enrollment {EnrollmentId} status changed from {OldStatus} to {NewStatus}",
                enrollment.EnrollmentId,
                oldStatus,
                normalizedStatus);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                return false;

            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e =>
                    e.EnrollmentId == id &&
                    !e.IsDeleted);

            if (enrollment == null)
            {
                _logger.LogWarning(
                    "Enrollment deletion failed: enrollment {EnrollmentId} was not found",
                    id);

                return false;
            }

            enrollment.IsDeleted = true;
            enrollment.DeletedAt = DateTime.UtcNow;
            enrollment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Enrollment {EnrollmentId} soft-deleted successfully",
                enrollment.EnrollmentId);

            return true;
        }

        public async Task<int?> GetTrackInstructorIdAsync(int trackId)
        {
            if (trackId <= 0)
                return null;

            return await _context.TrainingTracks
                .AsNoTracking()
                .Where(t =>
                    t.TrainingTrackId == trackId &&
                    !t.IsDeleted)
                .Select(t => (int?)t.InstructorId)
                .FirstOrDefaultAsync();
        }
    }
}
