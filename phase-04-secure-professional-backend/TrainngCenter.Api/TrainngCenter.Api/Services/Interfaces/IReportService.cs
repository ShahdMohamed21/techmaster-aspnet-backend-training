using TrainngCenter.Api.DTOs.Instructors;
using TrainngCenter.Api.DTOs.Payments;
using TrainngCenter.Api.DTOs.Reports;
using TrainngCenter.Api.DTOs.Tracks;

public interface IReportService
{
    Task<DashboardSummaryResponse> GetDashboardSummaryAsync();

    Task<List<UnpaidEnrollmentResponse>> GetUnpaidEnrollmentsAsync(int? instructorId = null);

    Task<List<TrackCapacityResponse>> GetTrackCapacityAsync( int? instructorId = null);

    Task<RevenueSummaryResponse> GetRevenueSummaryAsync();

    Task<List<RevenueByTrackResponse>> GetRevenueByTrackAsync();

    Task<List<TopTrackResponse>> GetTopTracksAsync( int top = 5,int? instructorId = null);

    Task<List<InstructorWorkloadResponse>> GetInstructorWorkloadAsync( int? instructorId = null);

    Task<List<StudentWithoutPaymentResponse>>GetStudentsWithoutPaymentsAsync(int? instructorId = null);
}