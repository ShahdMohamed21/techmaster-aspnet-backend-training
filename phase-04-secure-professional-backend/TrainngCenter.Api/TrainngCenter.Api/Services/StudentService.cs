using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Students;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Services.Interfaces;
using TrainngCenter.Api.DTOs.Students;

namespace TrainingCenter.Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public StudentService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<object> GetAllAsync(string? search, bool? isActive, int page, int pageSize)
        {
            if (page < 1)
                page = 1;

            if (pageSize < 1)
                pageSize = 10;

            var query = _context.Students.AsNoTracking().Where(s => !s.IsDeleted);

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(s =>
                    s.FullName.Contains(search) ||
                    s.Email.Contains(search) ||
                    s.PhoneNumber.Contains(search));
            }

            if (isActive.HasValue)
            {
                query = query.Where(s => s.IsActive == isActive.Value);
            }

            var totalCount = await query.CountAsync();

            var students = await query.OrderBy(s => s.StudentId) .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
             var totalPages = (int)(
              totalCount / (double)pageSize);

            return new
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                totalPages = totalPages,
                Data = _mapper.Map<List<StudentListItemResponse>>(students)
            };
        }

        public async Task<StudentDetailsResponse?> GetByIdAsync(int id)
        {
            var student = await _context.Students.AsNoTracking().Include(s => s.Enrollments).FirstOrDefaultAsync(s =>
                    s.StudentId == id &&
                    !s.IsDeleted);

            if (student == null)
                return null;

            var response = _mapper.Map<StudentDetailsResponse>(student);

            response.TotalEnrollments = student.Enrollments.Count;

            response.ActiveEnrollments = student.Enrollments.Count(e =>
                e.Status == "Active");

            return response;
        }

        public async Task<(bool Success, string Message, StudentDetailsResponse? Data)> CreateAsync(CreateStudentRequest request)
        {
            var emailExists = await _context.Students .AnyAsync(s => s.Email == request.Email);

            if (emailExists)
                return (false, "A student with this email already exists", null);

            var student = _mapper.Map<Student>(request);

            student.CreatedAt = DateTime.UtcNow;
            student.IsActive = true;
            student.IsDeleted = false;

            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            var response = _mapper.Map<StudentDetailsResponse>(student);

            return (true, "Student created successfully", response);
        }

        public async Task<(bool Success, string Message)> UpdateAsync(int id, UpdateStudentRequest request)
        {
            var student = await _context.Students .FirstOrDefaultAsync(s =>  s.StudentId == id && !s.IsDeleted);

            if (student == null)
                return (false, "Student not found");

            var emailExists = await _context.Students.AnyAsync(s => s.Email == request.Email && s.StudentId != id);

            if (emailExists)
                return (false, "Another student already uses this email");

            _mapper.Map(request, student);

            student.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return (true, "Student updated successfully");
        }

        public async Task<(bool Success, string Message)> DeleteAsync(int id)
        {
            var student = await _context.Students .FirstOrDefaultAsync(s =>  s.StudentId == id && !s.IsDeleted);

            if (student == null)
                return (false, "Student not found");

            student.IsDeleted = true;
            student.IsActive = false;
            student.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return (true, "Student deleted successfully");
        }

        public async Task<StudentDetailsResponse?> GetMyProfileAsync(int studentId)
        {
            return await GetByIdAsync(studentId);
        }

        public async Task<List<MyEnrollmentResponse>> GetMyEnrollmentsAsync(int studentId)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .Where(e =>
                    e.StudentId == studentId &&
                    !e.IsDeleted)
                .Select(e => new MyEnrollmentResponse
                {
                    EnrollmentId = e.EnrollmentId,
                    TrainingTrackId = e.TrainingTrackId,
                    TrackTitle = e.TrainingTrack.Title,
                    EnrollmentStatus = e.Status,
                    ProgressPercentage = e.ProgressPercentage
                })
                .ToListAsync();
        }


        public async Task<List<MyPaymentResponse>> GetMyPaymentsAsync(int studentId)
        {
            return await _context.Payments
                .AsNoTracking()
                .Where(p => p.Enrollment.StudentId == studentId)
                .Select(p => new MyPaymentResponse
                {
                    PaymentId = p.PaymentId,
                    EnrollmentId = p.EnrollmentId,
                    Amount = p.Amount,
                    PaymentMethod = p.PaymentMethod,
                    PaymentDate = p.PaymentDate,
                    PaymentStatus = p.PaymentStatus,
                    ReferenceNumber = p.ReferenceNumber
                })
                .ToListAsync();
        }

        public async Task<(bool Success, string Message)> UpdateMyProfileAsync(int studentId,UpdateMyProfileRequest request)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s =>
                s.StudentId == studentId &&
                !s.IsDeleted &&
                s.IsActive);

            if (student == null)
                return (false, "Student not found or inactive");

            var emailExists = await _context.Students.AnyAsync(s =>
                s.Email == request.Email &&
                s.StudentId != studentId &&
                !s.IsDeleted);

            if (emailExists)
                return (false, "Another student already uses this email");

            student.FullName = request.FullName;
            student.Email = request.Email;
            student.PhoneNumber = request.PhoneNumber;
            student.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return (true, "Profile updated successfully");
        }





    }
}