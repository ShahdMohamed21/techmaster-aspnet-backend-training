using TrainngCenter.Api.DTOs.Reports;

namespace TrainngCenter.Api.Services.Interfaces
{

    public interface IReportService
    {
        Task<DashboardSummaryResponse> GetDashboardSummaryAsync();

        Task<List<UnpaidEnrollmentResponse>> GetUnpaidEnrollmentsAsync();

        Task<List<TrackCapacityResponse>> GetTrackCapacityAsync();

        Task<RevenueSummaryResponse> GetRevenueSummaryAsync();

        Task<List<RevenueByTrackResponse>> GetRevenueByTrackAsync();
    }
}

