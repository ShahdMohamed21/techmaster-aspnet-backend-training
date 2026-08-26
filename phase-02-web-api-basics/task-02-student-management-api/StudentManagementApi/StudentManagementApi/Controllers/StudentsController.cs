using Microsoft.AspNetCore.Mvc;
using StudentManagementApi.DTOs;
using StudentManagementApi.Services;

namespace StudentManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService studentService;
        public StudentsController(IStudentService _studentService)
        {
            studentService= _studentService;
            
        }
        [HttpPost]
        public IActionResult CreateStudent([FromBody] CreateStudentRequest request)
        {
            var student = studentService.CreateStudent(request);  // studentrespone
            return Created("", student);

        }
        [HttpGet]
        public IActionResult GetAllStudents(string? search, string? trackName, bool? isActive, int pageNumber, int pageSize)
        {
            var students=studentService.GetAllStudents(search, trackName, isActive, pageNumber, pageSize);
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student= studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound("Student Not Found");
            }
            return Ok(student);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id,[FromBody] UpdateStudentRequest request)
        {
            var student = studentService.UpdateStudent(id,request);
            if (student == null)
            {
                return NotFound("Student Not Found");
            }
            return Ok(student);
        }
        [HttpPatch("{id}/status")]
        public IActionResult UpdateStudentStatus( int id, [FromBody] UpdateStudentStatusRequest request)
        {
            var student = studentService.UpdateStudentStatus(id, request);

            if (student == null)
            {
                return NotFound("Student Not Found");
            }

            return Ok(new
            {
                message = request.IsActive? "Student activated successfully": "Student deactivated successfully",
                student
            });
        }
        [HttpGet("stats")]
        public IActionResult GetStudentStats()
        {
            var stats = studentService.GetStudentStats();

            return Ok(stats);
        }



    }
}
