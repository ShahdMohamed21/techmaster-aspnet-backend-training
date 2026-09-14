using TrainingCenter.Api.Entities.Enrollments;

namespace TrainingCenter.Api.Entities.Payments
{
    public class PaymentSummary
    {

        public int Id { get; set; }

        public int EnrollmentId { get; set; }

        public decimal TotalRequired { get; set; }

        public decimal TotalPaid { get; set; }

        public decimal RemainingAmount =>  TotalRequired - TotalPaid;

        public PaymentStatus PaymentStatus { get; set; }

        public Enrollment Enrollment { get; set; }
    }
}
