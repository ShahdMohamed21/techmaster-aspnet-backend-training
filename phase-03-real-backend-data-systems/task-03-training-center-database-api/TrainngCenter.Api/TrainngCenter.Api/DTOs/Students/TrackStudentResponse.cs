namespace TrainngCenter.Api.DTOs.Students
{
    public class TrackStudentResponse
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int EnrollmentId { get; set; }
        public string EnrollmentStatus { get; set; } = null!;
        public decimal ProgressPercentage { get; set; }
    }
}
