namespace TrainngCenter.Api.DTOs.Enrollments
{
    public class EnrollmentListItemResponse
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public int TrainingTrackId { get; set; }
        public string TrainingTrackTitle { get; set; } = null!;
        public string Status { get; set; } = null!;
        public decimal ProgressPercentage { get; set; }
    }
}
