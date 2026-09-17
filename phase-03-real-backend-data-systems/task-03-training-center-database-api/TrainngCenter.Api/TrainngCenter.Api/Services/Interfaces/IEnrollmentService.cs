using TrainngCenter.Api.DTOs.Enrollments;
using TrainngCenter.Api.DTOs.Students;

namespace TrainingCenter.Api.Services.Interfaces
{
    public interface IEnrollmentService
    {
        Task<object> GetAllAsync(string? status,int? trackId, int? studentId, string? paymentStatus);

        Task<EnrollmentDetailsResponse?> GetByIdAsync(int id);

        Task<EnrollmentDetailsResponse> CreateAsync(CreateEnrollmentRequest request);

        Task<bool> UpdateStatusAsync(int id, string status);

        Task<List<EnrollmentListItemResponse>> GetStudentEnrollmentsAsync(int studentId);

        Task<List<TrackStudentResponse>> GetTrackStudentsAsync( int trackId);
    }
}