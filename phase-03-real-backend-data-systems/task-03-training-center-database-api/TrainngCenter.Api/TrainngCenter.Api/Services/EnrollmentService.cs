using Microsoft.EntityFrameworkCore;
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

        public EnrollmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedEnrollmentResponse> GetAllAsync(string? status,int? trackId,int? studentId,string? paymentStatus, int pageNumber,int pageSize)
        {
            var query = _context.Enrollments
                .AsNoTracking()
                .Where(e => !e.IsDeleted);

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(e => e.Status == status);
            }

            if (trackId.HasValue)
            {
                query = query.Where(e =>
                    e.TrainingTrackId == trackId.Value);
            }

            if (studentId.HasValue)
            {
                query = query.Where(e =>
                    e.StudentId == studentId.Value);
            }

            if (!string.IsNullOrWhiteSpace(paymentStatus))
            {
                query = query.Where(e =>
                    e.Payments.Any(p =>
                        p.PaymentStatus == paymentStatus));
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
            var enrollment = await _context.Enrollments
                .AsNoTracking()
                .Where(e => !e.IsDeleted)
                .Where(e => e.EnrollmentId == id)
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
                        .Select(p => new TrainngCenter.Api.DTOs.Payments.PaymentResponse
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
            var student = await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(s =>
                    s.StudentId == request.StudentId &&
                    s.IsActive &&
                    !s.IsDeleted);

            if (student == null)
            {
                throw new ArgumentException(
                    "Student does not exist, is inactive, or has been deleted");
            }

            var track = await _context.TrainingTracks
                .AsNoTracking()
                .FirstOrDefaultAsync(t =>
                    t.TrainingTrackId == request.TrainingTrackId &&
                    !t.IsDeleted);

            if (track == null)
            {
                throw new ArgumentException(
                    "Training track was not found");
            }

            if (track.Status != "Open")
            {
                throw new ArgumentException(
                    "Enrollment is allowed only for open tracks");
            }

            var alreadyEnrolled = await _context.Enrollments
                .AnyAsync(e =>
                    e.StudentId == request.StudentId &&
                    e.TrainingTrackId == request.TrainingTrackId &&
                    !e.IsDeleted &&
                    e.Status != "Cancelled");

            if (alreadyEnrolled)
            {
                throw new ArgumentException(
                    "This student is already enrolled in this track");
            }

            var activeEnrollments = await _context.Enrollments
                .CountAsync(e =>
                    e.TrainingTrackId == request.TrainingTrackId &&
                    !e.IsDeleted &&
                    e.Status == "Active");

            if (activeEnrollments >= track.Capacity)
            {
                throw new ArgumentException(
                    "The training track has reached its capacity");
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

            return (await GetByIdAsync(enrollment.EnrollmentId))!;
        }

        public async Task<List<EnrollmentListItemResponse>?>
            GetStudentEnrollmentsAsync(int studentId)
        {
            var studentExists = await _context.Students
                .AsNoTracking()
                .AnyAsync(s =>
                    s.StudentId == studentId &&
                    !s.IsDeleted);

            if (!studentExists)
            {
                return null;
            }

            var enrollments = await _context.Enrollments
                .AsNoTracking()
                .Where(e =>
                    e.StudentId == studentId &&
                    !e.IsDeleted)
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
            var students = await _context.Enrollments
                .AsNoTracking()
                .Where(e =>
                    e.TrainingTrackId == trackId &&
                    !e.IsDeleted)
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

        public async Task<bool> UpdateStatusAsync( int id, string status)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e =>
                    e.EnrollmentId == id &&
                    !e.IsDeleted);

            if (enrollment == null)
            {
                return false;
            }

            if (status != "Completed" &&
                status != "Cancelled")
            {
                throw new ArgumentException(
                    "Status must be Completed or Cancelled");
            }

            enrollment.Status = status;
            enrollment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var enrollment = await _context.Enrollments
                .FirstOrDefaultAsync(e =>
                    e.EnrollmentId == id &&
                    !e.IsDeleted);

            if (enrollment == null)
            {
                return false;
            }

            enrollment.IsDeleted = true;
            enrollment.DeletedAt = DateTime.UtcNow;
            enrollment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
