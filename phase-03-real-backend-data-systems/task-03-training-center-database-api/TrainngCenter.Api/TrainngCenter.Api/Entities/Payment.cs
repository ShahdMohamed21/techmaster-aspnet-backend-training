namespace TrainingCenter.Api.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }

        public int EnrollmentId { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = null!;

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public string PaymentStatus { get; set; } = null!;

        public string ReferenceNumber { get; set; } = null!;

        public string? Notes { get; set; }

        // Navigation Property

        public Enrollment Enrollment { get; set; } = null!;
    }
}