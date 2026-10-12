
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Students;
using TrainingCenter.Api.Entities;
using TrainngCenter.Api.DTOs.Tracks;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Services
{
    public class TrackService : ITrackService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<TrackService> _logger;
        private readonly IAuditLogService _auditLogService;

        public TrackService(ApplicationDbContext context,IMapper mapper,ILogger<TrackService> logger,IAuditLogService auditLogService)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
            _auditLogService = auditLogService;
        }

        private static bool IsOccupyingSeat(Enrollment enrollment)
        {
            return !enrollment.IsDeleted &&
                   (enrollment.Status == "Active" ||
                    enrollment.Status == "Pending");
        }

        public async Task<(bool success, string message,TrackDetailsResponse? Data)> CreateAsync(CreateTrackRequest request)
        {
            if (request == null)
                return (false, "Request is required", null);

            if (string.IsNullOrWhiteSpace(request.Title))
                return (false, "Track title is required", null);

            if (string.IsNullOrWhiteSpace(request.Code))
                return (false, "Track code is required", null);

            if (string.IsNullOrWhiteSpace(request.Level))
                return (false, "Track level is required", null);

            if (string.IsNullOrWhiteSpace(request.Status))
                return (false, "Track status is required", null);

            if (request.Capacity <= 0)
                return (false, "Capacity must be greater than zero", null);

            if (request.EndDate <= request.StartDate)
                return (false, "End date must be after start date", null);

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(i =>
                    i.InstructorId == request.InstructorId &&
                    i.IsActive);

            if (instructor == null)
                return (false, "Active instructor not found", null);

            var normalizedCode = request.Code.Trim();

            var codeExists = await _context.TrainingTracks
                .AnyAsync(t => t.Code == normalizedCode);

            if (codeExists)
                return (false, "A track with this code already exists", null);

            request.Code = normalizedCode;
            request.Title = request.Title.Trim();

            var track = _mapper.Map<TrainingTrack>(request);

            track.CreatedAt = DateTime.UtcNow;
            track.IsDeleted = false;

            await _context.TrainingTracks.AddAsync(track);
            await _context.SaveChangesAsync();

            await _auditLogService.LogAsync(
                action: "TrackCreated",
                entityName: "TrainingTrack",
                entityId: track.TrainingTrackId,
                description: $"Training track '{track.Title}' was created.",
                metadata: new
                {
                    TrackCode = track.Code,
                    TrackLevel = track.Level,
                    InstructorId = track.InstructorId,
                    Capacity = track.Capacity
                }
            );

            _logger.LogInformation(
                "Training track {TrackId} created successfully",
                track.TrainingTrackId);

            return (
                true,
                "Training track created successfully",
                _mapper.Map<TrackDetailsResponse>(track));
        }

        public async Task<(bool success, string message)> DeleteAsync(int id)
        {
            var track = await _context.TrainingTracks
                .Include(t => t.Enrollments)
                .FirstOrDefaultAsync(t =>
                    t.TrainingTrackId == id &&
                    !t.IsDeleted);

            if (track == null)
                return (false, "Training track not found");

            var hasActiveEnrollments =
                track.Enrollments.Any(IsOccupyingSeat);

            if (hasActiveEnrollments)
            {
                return (
                    false,
                    "Track cannot be deleted because it has active enrollments");
            }

            track.IsDeleted = true;

            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Training track {TrackId} soft-deleted successfully",
                track.TrainingTrackId);

            return (true, "Training track deleted successfully");
        }

        public async Task<List<TrackListItemResponse>> GetAllAsync(string? Keyword,string? level,string? status,int? instructorid)
        {
            var query = _context.TrainingTracks
                .AsNoTracking()
                .Where(t => !t.IsDeleted)
                .Include(t => t.Instructor)
                .Include(t => t.Enrollments)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                var keyword = Keyword.Trim();

                query = query.Where(t =>
                    t.Title.Contains(keyword) ||
                    t.Code.Contains(keyword));
            }

            if (!string.IsNullOrWhiteSpace(level))
            {
                query = query.Where(t => t.Level == level.Trim());
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(t => t.Status == status.Trim());
            }

            if (instructorid.HasValue)
            {
                query = query.Where(t =>
                    t.InstructorId == instructorid.Value);
            }

            var tracks = await query
                .OrderBy(t => t.Title)
                .ToListAsync();

            return tracks.Select(t =>
            {
                var enrolledCount =
                    t.Enrollments.Count(IsOccupyingSeat);

                return new TrackListItemResponse
                {
                    TrainingTrackId = t.TrainingTrackId,
                    Title = t.Title,
                    Code = t.Code,
                    Level = t.Level,
                    Status = t.Status,
                    Capacity = t.Capacity,
                    EnrolledStudents = enrolledCount,
                    InstructorName = t.Instructor.FullName
                };
            }).ToList();
        }

        public async Task<TrackDetailsResponse?> GetByIdAsync(int id)
        {
            var track = await _context.TrainingTracks
                .AsNoTracking()
                .Include(t => t.Instructor)
                .Include(t => t.Enrollments)
                .FirstOrDefaultAsync(t =>
                    t.TrainingTrackId == id &&
                    !t.IsDeleted);

            if (track == null)
                return null;

            var enrolledCount =
                track.Enrollments.Count(IsOccupyingSeat);

            return new TrackDetailsResponse
            {
                TrainingTrackId = track.TrainingTrackId,
                Title = track.Title,
                Code = track.Code,
                Level = track.Level,
                Status = track.Status,
                Capacity = track.Capacity,
                StartDate = track.StartDate,
                EndDate = track.EndDate,
                InstructorId = track.InstructorId,
                InstructorName = track.Instructor.FullName,
                EnrolledStudents = enrolledCount,
                AvailableSeats = Math.Max(
                    0,
                    track.Capacity - enrolledCount)
            };
        }

        public async Task<List<StudentListItemResponse>?> GetStudentsAsync(int id)
        {
            var trackExists = await _context.TrainingTracks
                .AnyAsync(t =>
                    t.TrainingTrackId == id &&
                    !t.IsDeleted);

            if (!trackExists)
                return null;

            var students = await _context.Enrollments
                .AsNoTracking()
                .Where(e =>
                    e.TrainingTrackId == id &&
                    !e.IsDeleted &&
                    !e.Student.IsDeleted)
                .Select(e => new StudentListItemResponse
                {
                    StudentId = e.Student.StudentId,
                    FullName = e.Student.FullName,
                    Email = e.Student.Email,
                    PhoneNumber = e.Student.PhoneNumber,
                    IsActive = e.Student.IsActive,
                    EnrollmentStatus = e.Status,
                    EnrollmentDate = e.EnrollmentDate
                })
                .ToListAsync();

            return students;
        }

        public async Task<(bool success, string message)> UpdateAsync(int id, UpdateTrackRequest request)
        {
            if (request == null)
                return (false, "Request is required");

            var track = await _context.TrainingTracks
                .Include(t => t.Enrollments)
                .FirstOrDefaultAsync(t =>
                    t.TrainingTrackId == id &&
                    !t.IsDeleted);

            if (track == null)
                return (false, "Training track not found");

            if (string.IsNullOrWhiteSpace(request.Title))
                return (false, "Track title is required");

            if (string.IsNullOrWhiteSpace(request.Code))
                return (false, "Track code is required");

            if (string.IsNullOrWhiteSpace(request.Level))
                return (false, "Track level is required");

            if (string.IsNullOrWhiteSpace(request.Status))
                return (false, "Track status is required");

            if (request.Capacity <= 0)
                return (false, "Capacity must be greater than zero");

            if (request.EndDate <= request.StartDate)
                return (false, "End date must be after start date");

            var enrolledCount =
                track.Enrollments.Count(IsOccupyingSeat);

            if (request.Capacity < enrolledCount)
            {
                return (
                    false,
                    "Capacity cannot be less than current enrolled students");
            }

            var instructorExists = await _context.Instructors
                .AnyAsync(i =>
                    i.InstructorId == request.InstructorId &&
                    i.IsActive);

            if (!instructorExists)
                return (false, "Active instructor not found");

            var normalizedCode = request.Code.Trim();

            var codeExists = await _context.TrainingTracks
                .AnyAsync(t =>
                    t.Code == normalizedCode &&
                    t.TrainingTrackId != id &&
                    !t.IsDeleted);

            if (codeExists)
                return (false, "Another track already uses this code");

            request.Code = normalizedCode;
            request.Title = request.Title.Trim();

            _mapper.Map(request, track);

            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Training track {TrackId} updated successfully",
                track.TrainingTrackId);

            return (true, "Training track updated successfully");
        }

        public async Task<bool> AssignInstructorAsync(
            int trackId,
            int instructorId)
        {
            var track = await _context.TrainingTracks
                .FirstOrDefaultAsync(t =>
                    t.TrainingTrackId == trackId &&
                    !t.IsDeleted);

            if (track == null)
            {
                _logger.LogWarning(
                    "Instructor assignment failed: track {TrackId} not found",
                    trackId);

                return false;
            }

            var instructor = await _context.Instructors
                .FirstOrDefaultAsync(i =>
                    i.InstructorId == instructorId &&
                    i.IsActive);

            if (instructor == null)
            {
                _logger.LogWarning(
                    "Instructor assignment failed: instructor {InstructorId} not found or inactive",
                    instructorId);

                throw new ArgumentException(
                    "Instructor does not exist or is inactive");
            }

            track.InstructorId = instructorId;

            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Instructor {InstructorId} assigned to track {TrackId}",
                instructorId,
                trackId);

            return true;
        }
    }
}
