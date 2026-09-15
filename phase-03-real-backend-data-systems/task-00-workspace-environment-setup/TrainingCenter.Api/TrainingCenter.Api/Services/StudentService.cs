using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.Entities.Students;

namespace TrainingCenter.Api.Services;

public class StudentService : IStudentService
{
    private readonly AppDbContext _context;

    public StudentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Student> CreateAsync(Student student)
    {
        student.CreatedAt = DateTime.UtcNow;
        student.UpdatedAt = null;

        _context.Students.Add(student);

        await _context.SaveChangesAsync();

        return student;
    }

    public async Task<bool> UpdateAsync(Student student)
    {
        var existingStudent = await _context.Students.FirstOrDefaultAsync(s => s.Id == student.Id);

        if (existingStudent == null)
            return false;

        existingStudent.FullName = student.FullName;
        existingStudent.Email = student.Email;
        existingStudent.IsActive = student.IsActive;

        existingStudent.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }
}