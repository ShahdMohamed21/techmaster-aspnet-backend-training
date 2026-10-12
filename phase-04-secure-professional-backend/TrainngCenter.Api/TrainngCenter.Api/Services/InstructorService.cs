using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.Entities;
using TrainngCenter.Api.DTOs.Instructor;
using TrainngCenter.Api.DTOs.Instructors;
using TrainngCenter.Api.DTOs.Students;
using TrainngCenter.Api.DTOs.Tracks;
using TrainngCenter.Api.Entities;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public InstructorService(ApplicationDbContext context, IMapper mapper)
        {

            _context = context;
            _mapper = mapper;
        }

        public async Task<(bool Success, string Message, InstructorResponse? Data)> CreateAsync(CreateInstructorRequest request)
        {
            var existemail = await _context.Instructors.AnyAsync(i => i.Email == request.Email);
            if (existemail)
            {
                return (false, "Email Already Exist", null);
            }
            var instructor = _mapper.Map<Instructor>(request);
            instructor.CreatedAt = DateTime.UtcNow;
            instructor.IsActive = true;
            _context.Instructors.Add(instructor);
            await _context.SaveChangesAsync();

            return (true, "Instructor Craeted", _mapper.Map<InstructorResponse>(instructor));

        }

        public async Task<List<InstructorResponse>> GetAllAsync()
        {
            var instructors = await _context.Instructors.ToListAsync();
            var AllInstructors = _mapper.Map<List<InstructorResponse>>(instructors);
            return AllInstructors;

        }

        public async Task<InstructorResponse>? GetInstructorById(int Id)
        {
            var instructor = await _context.Instructors.FirstOrDefaultAsync(i => i.InstructorId == Id);
            if (instructor == null)
            {
                return null;
            }
            return _mapper.Map<InstructorResponse>(instructor);
        }

        public async Task<(bool Success, string Message)> UpdateAsync(int id, UpdateInstructorRequest request)
        {
            var ExistInstructor = await _context.Instructors.FirstOrDefaultAsync(i => i.InstructorId == id);
            if (ExistInstructor == null)
            {
                return (false, "Instructor Not Found");
            }
            var ExistEmail = await _context.Instructors.AnyAsync(i => i.Email == request.Email &&
                    i.InstructorId != id);
            if (ExistEmail)
            {
                return (false, "Email ALready Exist");
            }
            _mapper.Map(request, ExistInstructor);
            await _context.SaveChangesAsync();
            return (true, "Instructor Updated");

        }

        public async Task<List<TrackListItemResponse>?> GetTracksAsync(int id)
        {
            var exists = await _context.Instructors
                .AnyAsync(i => i.InstructorId == id);

            if (!exists)
                return null;

            var tracks = await _context.TrainingTracks
                .AsNoTracking()
                .Where(t =>
                    t.InstructorId == id &&
                    !t.IsDeleted)
                .Include(t => t.Instructor)
                .Include(t => t.Enrollments)
                .ToListAsync();

            return tracks.Select(t => new TrackListItemResponse
            {
                TrainingTrackId = t.TrainingTrackId,
                Title = t.Title,
                Code = t.Code,
                Level = t.Level,
                Status = t.Status,
                Capacity = t.Capacity,
                EnrolledStudents = t.Enrollments.Count,
                InstructorName = t.Instructor.FullName
            }).ToList();
        }
        public async Task<List<TrackListItemResponse>> GetMyTracksAsync(int instructorId)
        {
            return await _context.TrainingTracks
                .AsNoTracking()
                .Where(t =>
                    t.InstructorId == instructorId &&
                    !t.IsDeleted)
                .Include(t => t.Enrollments)
                .Select(t => new TrackListItemResponse
                {
                    TrainingTrackId = t.TrainingTrackId,
                    Title = t.Title,
                    Code = t.Code,
                    Level = t.Level,
                    Status = t.Status,
                    Capacity = t.Capacity,
                    EnrolledStudents = t.Enrollments.Count,
                    InstructorName = t.Instructor.FullName
                })
                .ToListAsync();
        }
        public async Task<List<TrackStudentResponse>?> GetTrackStudentsAsync(int instructorId,int trackId)
        {
            var trackExists = await _context.TrainingTracks.AnyAsync(t =>
                    t.TrainingTrackId == trackId &&
                    t.InstructorId == instructorId &&
                    !t.IsDeleted);

            if (!trackExists)
                return null;

            return await _context.Enrollments.AsNoTracking().Where(e =>e.TrainingTrackId == trackId &&!e.IsDeleted)
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
        }
        public async Task<SessionResponse> CreateSessionAsync(int instructorId,int trackId,CreateSessionRequest request)
        {
            var track = await _context.TrainingTracks
                .FirstOrDefaultAsync(t =>
                    t.TrainingTrackId == trackId &&
                    t.InstructorId == instructorId &&
                    !t.IsDeleted);

            if (track == null)
            {
                throw new UnauthorizedAccessException(
                    "You are not assigned to this training track");
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new ArgumentException("Session title is required");
            }

            var session = new TrackSession
            {
                TrainingTrackId = trackId,
                Title = request.Title.Trim(),
                Description = request.Description,
                MeetingLink = request.MeetingLink,
                IsCompleted = false,
                CreatedByInstructorId = instructorId,
                CreatedAt = DateTime.UtcNow
            };

            _context.TrackSessions.Add(session);
            await _context.SaveChangesAsync();

            return new SessionResponse
            {
                TrackSessionId = session.TrackSessionId,
                TrainingTrackId = session.TrainingTrackId,
                SessionDate = session.SessionDate,
                Title = session.Title,
                Description = session.Description,
                MeetingLink = session.MeetingLink,
                IsCompleted = session.IsCompleted,
                CreatedByInstructorId = session.CreatedByInstructorId,
                CreatedAt = session.CreatedAt,
                UpdatedAt = session.UpdatedAt
            };
        }
        public async Task<SessionResponse?> UpdateSessionAsync(int instructorId,int sessionId,UpdateSessionRequest request)
        {
            var session = await _context.TrackSessions.FirstOrDefaultAsync(s =>s.TrackSessionId == sessionId);

            if (session == null)
                return null;

            var ownsTrack = await _context.TrainingTracks.AnyAsync(t =>
                    t.TrainingTrackId == session.TrainingTrackId &&
                    t.InstructorId == instructorId &&
                    !t.IsDeleted);

            if (!ownsTrack)
            {
                throw new UnauthorizedAccessException("You are not assigned to this training track");
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new ArgumentException("Session title is required");
            }

            session.SessionDate = request.SessionDate;
            session.Title = request.Title.Trim();
            session.Description = request.Description;
            session.MeetingLink = request.MeetingLink;
            session.IsCompleted = request.IsCompleted;
            session.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new SessionResponse
            {
                TrackSessionId = session.TrackSessionId,
                TrainingTrackId = session.TrainingTrackId,
                SessionDate = session.SessionDate,
                Title = session.Title,
                Description = session.Description,
                MeetingLink = session.MeetingLink,
                IsCompleted = session.IsCompleted,
                CreatedByInstructorId = session.CreatedByInstructorId,
                CreatedAt = session.CreatedAt,
                UpdatedAt = session.UpdatedAt
            };
        }
        public async Task<List<TrackStudentResponse>> GetTrackProgressAsync(int instructorId,int trackId)
        {
            var trackExists = await _context.TrainingTracks
                .AnyAsync(t =>
                    t.TrainingTrackId == trackId &&
                    t.InstructorId == instructorId &&
                    !t.IsDeleted);

            if (!trackExists)
            {
                throw new UnauthorizedAccessException("You are not assigned to this training track");
            }

            return await _context.Enrollments.AsNoTracking()
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
        }


    }
}
  
