using StudentManagementApi.DTOs;
using StudentManagementApi.Models;

namespace StudentManagementApi.Services
{
   public interface IStudentService
    {
        public StudentResponse CreateStudent(CreateStudentRequest stu);
        public List<StudentResponse> GetAllStudents(string ?search , string? trackName ,bool? isActive, int pageNumber, int pageSize);
        public StudentResponse GetStudentById(int id);
        public StudentResponse? UpdateStudent(int id , UpdateStudentRequest request);
        public StudentResponse? UpdateStudentStatus(int id,UpdateStudentStatusRequest request);
        public StudentStatsResponse GetStudentStats();


    }
}
