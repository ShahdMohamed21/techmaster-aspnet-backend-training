namespace TrainngCenter.Api.DTOs.Payments
{
    public class PaymentResponse
    {
        public int PaymentId { get; set; }
        public int EnrollmentId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; } = null!;
        public string ReferenceNumber { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
