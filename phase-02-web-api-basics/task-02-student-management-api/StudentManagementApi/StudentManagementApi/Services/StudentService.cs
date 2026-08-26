using StudentManagementApi.DTOs;
using StudentManagementApi.Models;

namespace StudentManagementApi.Services
{
    public class StudentService : IStudentService
    {
         private List<Student> students = new List<Student>();
        Student student1 = new Student()
        {
            StudentId = 1,
            FullName = "Shahd Mohamed",
            Email = "ShahdMohamedd55@gmail.com",
            PhoneNumber = "01094671361",
            TrackName = ".NET",
            EnrollmentDate = DateTime.Now,
            GitHubProfileUrl = "Https://GitHub/Shahd.com",
            IsActive = true,
        };
        Student student2 = new Student()
        {
            StudentId = 2,
            FullName = "Ahmed Mohamed",
            Email = "Ahmedddmoha2@gmail.com",
            PhoneNumber = "01018187282",
            TrackName = "Flutter",
            EnrollmentDate = DateTime.Now,
            IsActive = false,
        };
        int NextId = 3;
        public StudentService()
        {
            students.Add(student1);
            students.Add(student2);
        }
        public StudentResponse CreateStudent(CreateStudentRequest stu)
        {
            var IsEmailExist=students.FirstOrDefault(x=> x.Email == stu.Email);
            if(IsEmailExist != null)
            {
                throw new Exception("Email Already Exist");
            }
            else
            {
                var student = new Student()
                {
                    StudentId = NextId++,
                    Email = stu.Email,
                    FullName = stu.FullName,
                    TrackName = stu.TrackName,
                    PhoneNumber = stu.PhoneNumber,
                    GitHubProfileUrl = stu.GitHubProfileUrl,
                    EnrollmentDate = stu.EnrollmentDate,
                    LinkedInProfileUrl = stu.LinkedInProfileUrl,
                    IsActive = stu.IsActive,
                };
                students.Add(student);
                var response = new StudentResponse()
                {
                    StudentId = student.StudentId,
                    Email = student.Email,
                    FullName = student.FullName,
                    TrackName = student.TrackName,
                    PhoneNumber = student.PhoneNumber,
                    GitHubProfileUrl = student.GitHubProfileUrl,
                    EnrollmentDate = student.EnrollmentDate,
                    LinkedInProfileUrl = student.LinkedInProfileUrl,
                    IsActive = student.IsActive,
                };
                return response;
            }
            
        }

        public List<StudentResponse> GetAllStudents(string? search,string? trackName, bool? isActive, int pageNumber, int pageSize)
        {
            var query = students.AsQueryable(); // كدا معايا كل الطلاب
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>x.FullName.Contains(search) || x.Email.Contains(search));
            }
            if (!string.IsNullOrWhiteSpace(trackName))
            {
                query = query.Where(x => x.TrackName == trackName);
            }
            if (isActive.HasValue)
            {
                query = query.Where(x =>x.IsActive == isActive.Value);
            }
            var studentslist = query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new StudentResponse()
                {
                    StudentId = x.StudentId,
                    Email = x.Email,
                    FullName = x.FullName,
                    TrackName = x.TrackName,
                    PhoneNumber = x.PhoneNumber,
                    EnrollmentDate = x.EnrollmentDate,
                    GitHubProfileUrl = x.GitHubProfileUrl,
                    LinkedInProfileUrl = x.LinkedInProfileUrl,
                    IsActive = x.IsActive
                })
                .ToList();

            return studentslist;
        }

        public StudentResponse? GetStudentById(int id)
        {
            var stu = students.FirstOrDefault(x=> x.StudentId == id);
            if(stu == null)
            {
                return null;
            }
            StudentResponse student = new StudentResponse
            {
                StudentId = stu.StudentId,
                Email = stu.Email,
                FullName = stu.FullName,
                TrackName = stu.TrackName,
                PhoneNumber = stu.PhoneNumber,
                EnrollmentDate = stu.EnrollmentDate,
                GitHubProfileUrl = stu.GitHubProfileUrl,
                LinkedInProfileUrl = stu.LinkedInProfileUrl,
                IsActive = stu.IsActive
            };
            return student;
        }

        public StudentResponse? UpdateStudent(int id,UpdateStudentRequest request)
        {
            var stu = students.FirstOrDefault(x => x.StudentId == id);
            if(stu == null)
            {
                return null;
            }

            stu.Email=request.Email;
            stu.FullName=request.FullName;
            stu.TrackName=request.TrackName;
            stu.PhoneNumber=request.PhoneNumber;
            stu.EnrollmentDate=request.EnrollmentDate;
            stu.GitHubProfileUrl=request.GitHubProfileUrl;
            stu.LinkedInProfileUrl=request.LinkedInProfileUrl;
            stu.IsActive = request.IsActive;

            StudentResponse student = new StudentResponse
            {
                StudentId = stu.StudentId,
                Email = stu.Email,
                FullName = stu.FullName,
                TrackName = stu.TrackName,
                PhoneNumber = stu.PhoneNumber,
                EnrollmentDate = stu.EnrollmentDate,
                GitHubProfileUrl = stu.GitHubProfileUrl,
                LinkedInProfileUrl = stu.LinkedInProfileUrl,
                IsActive = stu.IsActive
            };
            return student;

        }
        public StudentResponse? UpdateStudentStatus(int id,UpdateStudentStatusRequest request)
        {
            var stu = students.FirstOrDefault(x => x.StudentId == id);

            if (stu == null)
            {
                return null;
            }

            stu.IsActive = request.IsActive;

            StudentResponse student = new StudentResponse
            {
                StudentId = stu.StudentId,
                Email = stu.Email,
                FullName = stu.FullName,
                TrackName = stu.TrackName,
                PhoneNumber = stu.PhoneNumber,
                EnrollmentDate = stu.EnrollmentDate,
                GitHubProfileUrl = stu.GitHubProfileUrl,
                LinkedInProfileUrl = stu.LinkedInProfileUrl,
                IsActive = stu.IsActive
            };

            return student;
        }
        public StudentStatsResponse GetStudentStats()
        {
            var stats = new StudentStatsResponse
            {
                TotalStudents = students.Count,

                ActiveStudents = students.Count(x => x.IsActive),

                InactiveStudents = students.Count(x => !x.IsActive),

                StudentsByTrack = students.GroupBy(x => x.TrackName).ToDictionary( x => x.Key, x => x.Count()
                )
            };

            return stats;
        }




    }
    }

