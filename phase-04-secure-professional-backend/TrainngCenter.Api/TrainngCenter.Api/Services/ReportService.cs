using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainngCenter.Api.DTOs.Instructors;
using TrainngCenter.Api.DTOs.Payments;
using TrainngCenter.Api.DTOs.Reports;
using TrainngCenter.Api.DTOs.Tracks;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Services
{
    public class ReportService : IReportService
    {
        private readonly ApplicationDbContext _context;

        public ReportService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<DashboardSummaryResponse> GetDashboardSummaryAsync()
        {
            return new DashboardSummaryResponse
            {
                TotalStudents = await _context.Students.CountAsync(s => !s.IsDeleted),

                ActiveStudents = await _context.Students.CountAsync(s => s.IsActive && !s.IsDeleted),

                TotalInstructors = await _context.Instructors.CountAsync(),

                TotalTracks = await _context.TrainingTracks.CountAsync(t => !t.IsDeleted),

                OpenTracks = await _context.TrainingTracks.CountAsync(t =>!t.IsDeleted &&t.Status == "Open"),

                TotalEnrollments = await _context.Enrollments.CountAsync(),

                ActiveEnrollments = await _context.Enrollments.CountAsync(e => e.Status == "Active"),

                TotalPayments = await _context.Payments.CountAsync(),

                TotalRevenue = await _context.Payments .Where(p => p.PaymentStatus == "Paid").SumAsync(p => p.Amount)
            };
        }

        public async Task<List<UnpaidEnrollmentResponse>>
            GetUnpaidEnrollmentsAsync(int? instructorId = null)
        {
            var query = _context.Enrollments
                .AsNoTracking()
                .Include(e => e.Student)
                .Include(e => e.TrainingTrack)
                .Include(e => e.Payments)
                .Where(e =>
                    !e.Payments.Any(p =>
                        p.PaymentStatus == "Paid"));
            if (instructorId.HasValue)
            {
                query = query.Where(e =>
                    e.TrainingTrack.InstructorId == instructorId.Value);
            }

            var enrollments = await query.ToListAsync();

            var result = enrollments
                .Select(e => new UnpaidEnrollmentResponse
                {
                    EnrollmentId = e.EnrollmentId,
                    StudentId = e.StudentId,
                    StudentName = e.Student.FullName,
                    TrainingTrackId = e.TrainingTrackId,
                    TrainingTrackTitle = e.TrainingTrack.Title,

                    TotalPaid = e.Payments
                        .Where(p => p.PaymentStatus == "Paid")
                        .Sum(p => p.Amount),

                    PaymentStatus = e.Payments.Any()
                        ? "Pending or Failed"
                        : "Unpaid"
                })
                .ToList();

            return result;
        }

        public async Task<List<TrackCapacityResponse>>
            GetTrackCapacityAsync(int? instructorId = null)
        {
            var query = _context.TrainingTracks
                .AsNoTracking()
                .Where(t => !t.IsDeleted);

            if (instructorId.HasValue)
            {
                query = query.Where(t =>
                    t.InstructorId == instructorId.Value);
            }

            var tracks = await query
                .Include(t => t.Enrollments)
                .ToListAsync();

            var result = tracks
                .Select(t => new TrackCapacityResponse
                {
                    TrainingTrackId = t.TrainingTrackId,
                    Title = t.Title,
                    Capacity = t.Capacity,

                    EnrolledStudents = t.Enrollments
                        .Count(e => e.Status == "Active")
                }).ToList();

            foreach (var track in result)
            {
                track.AvailableSeats =
                    track.Capacity - track.EnrolledStudents;

                track.OccupancyPercentage =
                    track.Capacity > 0
                        ? (decimal)track.EnrolledStudents
                            / track.Capacity * 100
                        : 0;
            }

            return result;
        }
        public async Task<RevenueSummaryResponse> GetRevenueSummaryAsync()
        {
            var totalRevenue = await _context.Payments
                .Where(p => p.PaymentStatus == "Paid")
                .SumAsync(p => (decimal?)p.Amount) ?? 0;

            var pendingAmount = await _context.Payments
                .Where(p => p.PaymentStatus == "Pending")
                .SumAsync(p => (decimal?)p.Amount) ?? 0;

            var paidCount = await _context.Payments
                .CountAsync(p => p.PaymentStatus == "Paid");

            var pendingCount = await _context.Payments
                .CountAsync(p => p.PaymentStatus == "Pending");

            return new RevenueSummaryResponse
            {
                TotalRevenue = totalRevenue,
                TotalPaidPayments = paidCount,
                PendingAmount = pendingAmount,
                PendingPayments = pendingCount
            };
        }

 
        public async Task<List<RevenueByTrackResponse>>
            GetRevenueByTrackAsync()
        {
            var result = await _context.TrainingTracks
                .AsNoTracking()
                .Where(t => !t.IsDeleted)
                .Select(t => new RevenueByTrackResponse
                {
                    TrainingTrackId = t.TrainingTrackId,
                    TrackTitle = t.Title,

                    TotalRevenue = t.Enrollments
                        .SelectMany(e => e.Payments)
                        .Where(p => p.PaymentStatus == "Paid")
                        .Sum(p => (decimal?)p.Amount) ?? 0,

                    PaidPaymentsCount = t.Enrollments
                        .SelectMany(e => e.Payments)
                        .Count(p => p.PaymentStatus == "Paid")
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<TopTrackResponse>>
            GetTopTracksAsync(
                int top = 5,
                int? instructorId = null)
        {
            var query = _context.Enrollments
                .AsNoTracking()
                .Where(e =>
                    e.Status == "Active" &&
                    !e.Student.IsDeleted &&
                    !e.TrainingTrack.IsDeleted);

            if (instructorId.HasValue)
            {
                query = query.Where(e =>
                    e.TrainingTrack.InstructorId == instructorId.Value);
            }

            var result = await query
                .GroupBy(e => new
                {
                    e.TrainingTrackId,
                    e.TrainingTrack.Title
                })
                .Select(g => new TopTrackResponse
                {
                    TrainingTrackId = g.Key.TrainingTrackId,
                    Title = g.Key.Title,
                    ActiveEnrollmentCount = g.Count()
                })
                .OrderByDescending(x => x.ActiveEnrollmentCount)
                .Take(top)
                .ToListAsync();

            return result;
        }
        public async Task<List<InstructorWorkloadResponse>>
            GetInstructorWorkloadAsync(
                int? instructorId = null)
        {
            var query = _context.TrainingTracks
                .AsNoTracking()
                .Where(t => !t.IsDeleted);

            if (instructorId.HasValue)
            {
                query = query.Where(t =>
                    t.InstructorId == instructorId.Value);
            }

            var result = await query
                .GroupBy(t => new
                {
                    t.InstructorId,
                    t.Instructor.FullName
                })
                .Select(g => new InstructorWorkloadResponse
                {
                    InstructorId = g.Key.InstructorId,
                    InstructorName = g.Key.FullName,

                    TrackCount = g.Count(),

                    ActiveStudentCount = g
                        .SelectMany(t => t.Enrollments)
                        .Count(e =>
                            e.Status == "Active" &&
                            !e.Student.IsDeleted)
                })
                .ToListAsync();

            return result;
        }

        public async Task<List<StudentWithoutPaymentResponse>>
            GetStudentsWithoutPaymentsAsync(
                int? instructorId = null)
        {
            var query = _context.Enrollments
                .AsNoTracking()
                .Where(e =>
                    (e.Status == "Active" ||
                     e.Status == "Pending") &&
                    !e.Student.IsDeleted &&
                    !e.Payments.Any());

            if (instructorId.HasValue)
            {
                query = query.Where(e =>
                    e.TrainingTrack.InstructorId == instructorId.Value);
            }

            var result = await query
                .Select(e => new StudentWithoutPaymentResponse
                {
                    StudentId = e.Student.StudentId,
                    FullName = e.Student.FullName,
                    Email = e.Student.Email,
                    Phone = e.Student.PhoneNumber
                })
                .Distinct()
                .ToListAsync();

            return result;
        }
    }
}