namespace TrainngCenter.Api.DTOs.Reports
{
    public class DashboardSummaryResponse
    {
        public int TotalStudents { get; set; }
        public int ActiveStudents { get; set; }
        public int TotalInstructors { get; set; }
        public int TotalTracks { get; set; }
        public int OpenTracks { get; set; }
        public int TotalEnrollments { get; set; }
        public int ActiveEnrollments { get; set; }
        public int TotalPayments { get; set; }
        public decimal TotalRevenue { get; set; }
    }
}
