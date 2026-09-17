namespace TrainingCenter.Api.DTOs.Students
{
    public class StudentListItemResponse
    {
        public int StudentId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public string EnrollmentStatus { get; set; }
        public DateTime EnrollmentDate { get; set; }

    }
}