using StudentManagementApi.DTOs;
using StudentManagementApi.Models;

namespace StudentManagementApi.Services
{
   public interface IStudentService
    {
        public StudentResponse CreateStudent(CreateStudentRequest stu);
        public List<StudentResponse> GetAllStudents(string ?search , string? trackName ,bool? isActive, int pageNumber, int pageSize);

    }
}
