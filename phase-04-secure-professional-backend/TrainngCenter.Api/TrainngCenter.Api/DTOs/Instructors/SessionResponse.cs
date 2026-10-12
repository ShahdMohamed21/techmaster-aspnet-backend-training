namespace TrainngCenter.Api.DTOs.Instructor
{
    public class SessionResponse
    {
        public int TrackSessionId { get; set; }

        public int TrainingTrackId { get; set; }

        public DateTime SessionDate { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? MeetingLink { get; set; }

        public bool IsCompleted { get; set; }

        public int CreatedByInstructorId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}