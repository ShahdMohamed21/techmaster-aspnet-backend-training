using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.Entities;
using TrainngCenter.Api.DTOs.Instructors;
using TrainngCenter.Api.DTOs.Tracks;
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
       
    }
}
  
