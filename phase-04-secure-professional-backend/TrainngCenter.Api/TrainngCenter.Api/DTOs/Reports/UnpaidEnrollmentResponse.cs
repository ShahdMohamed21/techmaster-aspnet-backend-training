namespace TrainngCenter.Api.DTOs.Reports
{
    public class UnpaidEnrollmentResponse
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;
        public int TrainingTrackId { get; set; }
        public string TrainingTrackTitle { get; set; } = null!;
        public decimal TotalPaid { get; set; }
        public string PaymentStatus { get; set; } = null!;
    }
}
