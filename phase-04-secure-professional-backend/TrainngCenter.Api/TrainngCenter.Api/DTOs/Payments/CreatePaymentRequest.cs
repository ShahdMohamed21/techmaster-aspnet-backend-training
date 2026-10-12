
using System.ComponentModel.DataAnnotations;

namespace TrainngCenter.Api.DTOs.Payments
{
    public class CreatePaymentRequest
    {
        [Range(1, int.MaxValue)]
        public int EnrollmentId { get; set; }

        [Range(typeof(decimal), "0.01", "79228162514264337593543950335")]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentMethod { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string ReferenceNumber { get; set; } = string.Empty;

        public string? Notes { get; set; }
    }
}
