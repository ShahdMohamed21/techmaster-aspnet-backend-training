namespace TrainngCenter.Api.DTOs.Payments
{
    public class CreatePaymentRequest
    {
        public int EnrollmentId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public string ReferenceNumber { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
