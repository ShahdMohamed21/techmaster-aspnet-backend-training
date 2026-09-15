using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Common;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Students;
using TrainingCenter.Api.DTOs.TrainingTracks;
using TrainingCenter.Api.Entities.DTOs.Students;
using TrainingCenter.Api.Entities.Students;
using TrainingCenter.Api.Services;

namespace TrainingCenter.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IStudentService _studentService;
        public StudentsController(AppDbContext context, IStudentService studentService)
        {
            _context = context;
            _studentService = studentService;

        }
        // هنا بستبعد ال delete 

       /* [HttpGet]
        public async Task<IActionResult> GetStudents([FromQuery] bool includeDeleted = false)
        {
            var query = _context.Students.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(s => !s.IsDeleted);
            }

            var students = await query.ToListAsync();

            return Ok(students);
        }
       */
        //projection Drill-09
      /*  [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var students = await _context.Students
                .Where(s => !s.IsDeleted)
                .Select(s => new StudentListItemDto
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Email = s.Email,
                    IsActive = s.IsActive
                })
                .ToListAsync();

            return Ok(students);
        } */

        [HttpGet]
        public async Task<IActionResult> GetStudents([FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 5)
        {
            if (pageNumber <= 0)
                return BadRequest("pageNumber must be greater than 0");

            if (pageSize < 1 || pageSize > 50)
                return BadRequest("pageSize must be between 1 and 50");

            var query = _context.Students.Where(s => !s.IsDeleted);

            var totalCount = await query.CountAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var students = await query
                .OrderBy(s => s.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new StudentListItemDto
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Email = s.Email,
                    IsActive = s.IsActive
                })
                .ToListAsync();

            var result = new PaginationResult<StudentListItemDto>
            {
                Items = students,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = totalPages
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await _studentService.GetByIdAsync(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }
        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentDto dto)
        {
            var student = new Student
            {
                FullName = dto.FullName,
                Email = dto.Email,
                IsActive = dto.IsActive
            };

            var createdStudent = await _studentService.CreateAsync(student);

            return CreatedAtAction(
                nameof(GetStudentById),
                new { id = createdStudent.Id },
                createdStudent);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent( int id, UpdateStudentDto dto)
        {
            var student = new Student
            {
                Id = id,
                FullName = dto.FullName,
                Email = dto.Email,
                IsActive = dto.IsActive
            };
            var updated = await _studentService.UpdateAsync(student);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{id}/tracks")]
        public async Task<IActionResult> GetTracks(int id)
        {
            var studentExists = await _context.Students
                .AnyAsync(s => s.Id == id);

            if (!studentExists)
                return NotFound();

            var tracks = await _context.Enrollments
                .Where(e => e.StudentId == id)
                .Select(e => new TrainingTrackResponseDto
                {
                    Id = e.TrainingTrack.Id,
                    Name = e.TrainingTrack.Name
                })
                .ToListAsync();

            return Ok(tracks);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students
                .FirstOrDefaultAsync(s => s.Id == id);

            if (student == null)
                return NotFound();

            student.IsDeleted = true;
            student.DeletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
