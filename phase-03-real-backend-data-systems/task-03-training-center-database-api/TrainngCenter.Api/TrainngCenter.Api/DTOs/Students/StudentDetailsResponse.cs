namespace TrainingCenter.Api.DTOs.Students
{
    public class StudentDetailsResponse
    {
        public int StudentId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int TotalEnrollments { get; set; }
        public int ActiveEnrollments { get; set; }
    }
}