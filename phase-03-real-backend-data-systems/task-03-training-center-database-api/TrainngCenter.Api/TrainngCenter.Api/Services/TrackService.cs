using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Students;
using TrainingCenter.Api.Entities;
using TrainngCenter.Api.DTOs.Tracks;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Services
{
    public class TrackService:ITrackService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public TrackService(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<(bool success, string message, TrackDetailsResponse? Data)> CreateAsync(CreateTrackRequest request)
        {
            if(request.Capacity<=0)
            {
                return (false, "Capacity Must Be Greater Than 0", null);
            }
            if(request.EndDate<=request.StartDate)
            {
                return (false, "End Date Must Be After Start Date", null);
            }
            var instructor = await _context.Instructors.FirstOrDefaultAsync(i => i.InstructorId == request.InstructorId && i.IsActive);
            if (instructor == null)
            {
                return (false, "Active instructor not found", null);
            }
            var codeExists = await _context.TrainingTracks.AnyAsync(t => t.Code == request.Code);
            if (codeExists)
            {
                return (false, "A track with this code already exists", null);
            }
            var track = _mapper.Map<TrainingTrack>(request);
            track.CreatedAt=DateTime.UtcNow;
            track.IsDeleted = false;
            await _context.TrainingTracks.AddAsync(track);
            await _context.SaveChangesAsync();
            return (true, "Training Track Craeted Successfully", _mapper.Map<TrackDetailsResponse>(track));
        }

       public async Task<(bool success, string message)> DeleteAsync(int id)
        {
            var track = await _context.TrainingTracks.Include(t => t.Enrollments).FirstOrDefaultAsync(t => t.TrainingTrackId == id &&t.IsDeleted);
            if (track == null)
            {
                return (false, "Training track not found");
            }
            var hasActiveEnrollments = track.Enrollments.Any(e => e.Status == "Active");
            if (hasActiveEnrollments)
            {
                return (false,"Track cannot be deleted because it has active enrollments");
            }
            track.IsDeleted = true;

            await _context.SaveChangesAsync();

            return (true, "Training track deleted successfully");
        }

        public async Task<List<TrackListItemResponse>> GetAllAsync(string? Keyword, string? level, string? status, int? instructorid)
        {
            var query = _context.TrainingTracks
                 .AsNoTracking()
                 .Where(t => !t.IsDeleted)
                 .Include(t => t.Instructor)
                 .Include(t => t.Enrollments)
                 .AsQueryable();
            if(!string.IsNullOrWhiteSpace(Keyword))
            {
                query=query.Where(t=> t.Title.Contains(Keyword)||t.Code.Contains(Keyword));
            }
            if (!string.IsNullOrWhiteSpace(level))
            {
                query = query.Where(t => t.Level == level);
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(t => t.Status == status);
            }
            if(instructorid.HasValue)
            {
                query=query.Where(t=>t.InstructorId== instructorid.Value);
            }
            var tracks = await query
               .OrderBy(t => t.Title)
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

       public async Task<TrackDetailsResponse?> GetByIdAsync(int id)
        {
            var track=await _context.TrainingTracks.Include(t=>t.Instructor).Include(t=>t.Enrollments).FirstOrDefaultAsync(t=>t.TrainingTrackId==id&&!t.IsDeleted);
            if(track==null)
            {
                return null;
            }
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
                EnrolledStudents = track.Enrollments.Count,
                AvailableSeats = (track.Capacity - track.Enrollments.Count)
            };
            
        }

        public async Task<List<StudentListItemResponse>?> GetStudentsAsync(int id)
        {
            var track = await _context.TrainingTracks.FirstOrDefaultAsync(t => t.TrainingTrackId == id &&!t.IsDeleted);
            if (track == null)
                return null;
            var students = await _context.Enrollments
                .AsNoTracking()
                .Where(e =>
                    e.TrainingTrackId == id &&
                    e.Student.IsDeleted == false)
                .Include(e => e.Student)
                .Select(e => e.Student)
                .Distinct()
                .ToListAsync();

            return _mapper.Map<List<StudentListItemResponse>>(students);
        }

        public async Task<(bool success, string message)> UpdateAsync(int id, UpdateTrackRequest request)
        {
            var track = await _context.TrainingTracks.Include(t => t.Enrollments).FirstOrDefaultAsync(t => t.TrainingTrackId == id && !t.IsDeleted);

            if (track == null)
                return (false, "Training track not found");

            if (request.Capacity <= 0)
                return (false, "Capacity must be greater than zero");

            if (request.EndDate <= request.StartDate)
                return (false, "End date must be after start date");

            if (request.Capacity < track.Enrollments.Count)
                return (false,"Capacity cannot be less than current enrolled students");
            var instructorExists = await _context.Instructors.AnyAsync(i =>i.InstructorId == request.InstructorId && i.IsActive);
            if (!instructorExists)
                return (false, "Active instructor not found");

            var codeExists = await _context.TrainingTracks .AnyAsync(t => t.Code == request.Code &&t.TrainingTrackId != id);
            if (codeExists)
                return (false, "Another track already uses this code");
            _mapper.Map(request, track);
            await _context.SaveChangesAsync();
            return (true, "Training track updated successfully");
        }
    }
}
