using TrainingCenter.Api.Entities.Students;

namespace TrainingCenter.Api.Services;

public interface IStudentService
{
    Task<Student?> GetByIdAsync(int id);
    Task<Student> CreateAsync(Student student);
    Task<bool> UpdateAsync(Student student);
}