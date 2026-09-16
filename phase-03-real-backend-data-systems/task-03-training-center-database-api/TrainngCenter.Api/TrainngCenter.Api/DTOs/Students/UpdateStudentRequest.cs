namespace TrainingCenter.Api.DTOs.Students
{
    public class UpdateStudentRequest
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
    }
}