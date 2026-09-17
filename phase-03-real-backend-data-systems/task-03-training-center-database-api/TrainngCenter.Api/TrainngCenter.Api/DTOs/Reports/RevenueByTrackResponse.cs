namespace TrainngCenter.Api.DTOs.Reports
{
    public class RevenueByTrackResponse
    {
        public int TrainingTrackId { get; set; }
        public string TrackTitle { get; set; } = null!;
        public decimal TotalRevenue { get; set; }
        public int PaidPaymentsCount { get; set; }
    }
}
