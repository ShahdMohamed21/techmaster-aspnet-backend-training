namespace TrainingCenter.Api.Entities.DTOs.Students;

public class CreateStudentDto
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}