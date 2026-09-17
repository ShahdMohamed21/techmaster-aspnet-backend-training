namespace TrainngCenter.Api.DTOs.Tracks
{
    public class TrackListItemResponse
    {

        public int TrainingTrackId { get; set; }
        public string Title { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string Level { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int Capacity { get; set; }
        public int EnrolledStudents { get; set; }
        public string InstructorName { get; set; } = null!;
    }
}
