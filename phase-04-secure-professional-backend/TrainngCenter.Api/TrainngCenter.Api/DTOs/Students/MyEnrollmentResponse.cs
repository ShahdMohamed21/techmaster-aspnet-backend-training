namespace TrainngCenter.Api.DTOs.Students
{
    public class MyEnrollmentResponse
    {
        public int EnrollmentId { get; set; }
        public int TrainingTrackId { get; set; }
        public string TrackTitle { get; set; } = string.Empty;
        public string EnrollmentStatus { get; set; } = string.Empty;
        public decimal ProgressPercentage { get; set; }
    }
}
