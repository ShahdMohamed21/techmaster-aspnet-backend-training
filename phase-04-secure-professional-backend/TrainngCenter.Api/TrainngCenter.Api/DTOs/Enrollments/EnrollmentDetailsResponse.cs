using TrainngCenter.Api.DTOs.Payments;

namespace TrainngCenter.Api.DTOs.Enrollments
{
    public class EnrollmentDetailsResponse
    {
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }
        public string StudentName { get; set; } = null!;

        public int TrainingTrackId { get; set; }
        public string TrainingTrackTitle { get; set; } = null!;

        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; } = null!;
        public decimal ProgressPercentage { get; set; }
        public string? FinalResult { get; set; }

        public List<PaymentResponse> Payments { get; set; } = new();
    }
}
