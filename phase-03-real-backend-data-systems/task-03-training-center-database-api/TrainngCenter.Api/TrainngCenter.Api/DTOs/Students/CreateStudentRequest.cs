namespace TrainingCenter.Api.DTOs.Students
{
    public class CreateStudentRequest
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}