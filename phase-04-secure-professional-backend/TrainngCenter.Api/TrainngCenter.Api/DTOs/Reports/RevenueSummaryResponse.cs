namespace TrainngCenter.Api.DTOs.Reports
{
    public class RevenueSummaryResponse
    {
        public decimal TotalRevenue { get; set; }
        public int TotalPaidPayments { get; set; }
        public decimal PendingAmount { get; set; }
        public int PendingPayments { get; set; }
    }
}
