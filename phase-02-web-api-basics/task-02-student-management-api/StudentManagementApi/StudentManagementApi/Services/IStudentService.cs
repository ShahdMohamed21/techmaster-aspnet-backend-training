using StudentManagementApi.DTOs;
using StudentManagementApi.Models;

namespace StudentManagementApi.Services
{
   public interface IStudentService
    {
        public StudentResponse CreateStudent(CreateStudentRequest stu);

    }
}
