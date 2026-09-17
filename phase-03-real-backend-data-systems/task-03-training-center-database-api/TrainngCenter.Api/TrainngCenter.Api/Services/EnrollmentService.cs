using AutoMapper;
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
        private readonly IMapper _mapper;
        public EnrollmentService(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            
        }

       public async Task<EnrollmentDetailsResponse> CreateAsync(CreateEnrollmentRequest request)
        {
            var student=await _context.Students.FirstOrDefaultAsync(s=>s.StudentId==request.StudentId &&
                    s.IsActive &&
                    !s.IsDeleted);
            if(student==null)
            {
                throw new ArgumentException(
                    "Student does not exist, is inactive, or has been deleted");
            }
            var track = await _context.TrainingTracks
                .Include(t => t.Enrollments)
                .FirstOrDefaultAsync(t =>t.TrainingTrackId == request.TrainingTrackId &&!t.IsDeleted);

            if (track == null)
            {
                throw new ArgumentException("Training track was not found");
            }
            if (track.Status != "Open")
            {
                throw new ArgumentException("Enrollment is allowed only for open tracks");
            }
            var alreadyEnrolled = await _context.Enrollments.AnyAsync(e =>
                    e.StudentId == request.StudentId &&
                    e.TrainingTrackId == request.TrainingTrackId &&
                    e.Status != "Cancelled");

            if (alreadyEnrolled)
            {
                throw new ArgumentException("This student is already enrolled in this track");
            }
            var activeEnrollments = track.Enrollments.Count(e => e.Status == "Active");

            if (activeEnrollments >= track.Capacity)
            {
                throw new ArgumentException("The training track has reached its capacity");
            }
            var enrollment = _mapper.Map<Enrollment>(request);
            enrollment.EnrollmentDate = DateTime.UtcNow;
            enrollment.CreatedAt = DateTime.UtcNow;
            enrollment.Status = "Active";
            enrollment.ProgressPercentage = 0;

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return (await GetByIdAsync(enrollment.EnrollmentId))!;
        }

        public async Task<object> GetAllAsync(string? status, int? trackId, int? studentId, string? paymentStatus)
        {

            var query = _context.Enrollments
                .AsNoTracking()
                .Include(e => e.Student)
                .Include(e => e.TrainingTrack)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(e => e.Status == status);
            }

            if (trackId.HasValue)
            {
                query = query.Where(e => e.TrainingTrackId == trackId);
            }

            if (studentId.HasValue)
            {
                query = query.Where(e => e.StudentId == studentId);
            }

            if (!string.IsNullOrWhiteSpace(paymentStatus))
            {
                query = query.Where(e =>e.Payments.Any(p =>
                        p.PaymentStatus == paymentStatus));
            }

            var enrollments = await query.ToListAsync();

            return _mapper.Map<List<EnrollmentListItemResponse>>(enrollments);
        }

       public async Task<EnrollmentDetailsResponse?> GetByIdAsync(int id)
        {
            var enrollment = await _context.Enrollments
                 .AsNoTracking()
                 .Include(e => e.Student)
                 .Include(e => e.TrainingTrack)
                 .Include(e => e.Payments)
                 .FirstOrDefaultAsync(e => e.EnrollmentId == id);

            if (enrollment == null)
                return null;

            return _mapper.Map<EnrollmentDetailsResponse>(enrollment);
        }

        public async Task<List<EnrollmentListItemResponse>?> GetStudentEnrollmentsAsync(int studentId)
        {
            var student=await _context.Students.FirstOrDefaultAsync(s=>s.StudentId == studentId);
            if (student == null) return null;
            var enrollments = await _context.Enrollments
                .AsNoTracking()
                .Include(e => e.Student)
                .Include(e => e.TrainingTrack)
                .Where(e => e.StudentId == studentId)
                .ToListAsync();

            return _mapper.Map<List<EnrollmentListItemResponse>>(enrollments);
        }

        public async Task<List<TrackStudentResponse>> GetTrackStudentsAsync(int trackId)
        {
            var students = await _context.Enrollments
                .AsNoTracking()
                .Include(e => e.Student)
                .Where(e => e.TrainingTrackId == trackId)
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

       public async Task<bool> UpdateStatusAsync(int id, string status)
        {
            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.EnrollmentId == id);

            if (enrollment == null)
            {
                return false;
            }

            if (status != "Completed" && status != "Cancelled")
            {
                throw new ArgumentException(
                    "Status must be Completed or Cancelled");
            }

            if (enrollment.Status != "Active")
            {
                throw new ArgumentException(
                    "Only active enrollments can be completed or cancelled");
            }

            enrollment.Status = status;
            enrollment.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
