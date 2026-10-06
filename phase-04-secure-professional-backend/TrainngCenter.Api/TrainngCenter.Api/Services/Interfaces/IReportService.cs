using TrainngCenter.Api.DTOs.Instructors;
using TrainngCenter.Api.DTOs.Payments;
using TrainngCenter.Api.DTOs.Reports;
using TrainngCenter.Api.DTOs.Tracks;

namespace TrainngCenter.Api.Services.Interfaces
{

    public interface IReportService
    {
        Task<DashboardSummaryResponse> GetDashboardSummaryAsync();

        Task<List<UnpaidEnrollmentResponse>> GetUnpaidEnrollmentsAsync();

        Task<List<TrackCapacityResponse>> GetTrackCapacityAsync();

        Task<RevenueSummaryResponse> GetRevenueSummaryAsync();

        Task<List<RevenueByTrackResponse>> GetRevenueByTrackAsync();
        Task<List<TopTrackResponse>> GetTopTracksAsync(int top = 5);
        Task<List<InstructorWorkloadResponse>> GetInstructorWorkloadAsync();
        Task<List<StudentWithoutPaymentResponse>> GetStudentsWithoutPaymentsAsync();


    }
}

