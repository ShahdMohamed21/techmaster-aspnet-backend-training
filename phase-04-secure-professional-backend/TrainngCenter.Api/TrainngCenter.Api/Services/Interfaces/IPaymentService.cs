using TrainngCenter.Api.DTOs.Payments;

namespace TrainngCenter.Api.Services.Interfaces
{
    public interface IPaymentService
    {
       
            Task<List<PaymentResponse>> GetAllAsync(
                DateTime? fromDate,
                DateTime? toDate,
                string? status);

            Task<PaymentResponse?> GetByIdAsync(int id);

            Task<PaymentResponse> CreateAsync(
                CreatePaymentRequest request);

            Task<List<PaymentResponse>> GetEnrollmentPaymentsAsync(
                int enrollmentId);

            Task<bool> UpdateStatusAsync(
                int id,
                string status);
        
    }
}
