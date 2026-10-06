namespace TrainngCenter.Api.DTOs.Reports
{
    public class TrackCapacityResponse
    {
        public int TrainingTrackId { get; set; }
        public string Title { get; set; } = null!;
        public int Capacity { get; set; }
        public int EnrolledStudents { get; set; }
        public int AvailableSeats { get; set; }
        public decimal OccupancyPercentage { get; set; }
    }
}
