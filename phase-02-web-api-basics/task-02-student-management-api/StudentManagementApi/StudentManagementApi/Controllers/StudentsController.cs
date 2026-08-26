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

       
    }
}
