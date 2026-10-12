using TrainingCenter.Api.Entities;

namespace TrainngCenter.Api.Entities
{
    public class TrackSession
    {
        public int TrackSessionId { get; set; }

        public int TrainingTrackId { get; set; }

        public DateTime SessionDate { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? MeetingLink { get; set; }

        public bool IsCompleted { get; set; } = false;

        public int CreatedByInstructorId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public TrainingTrack TrainingTrack { get; set; } = null!;

        public Instructor CreatedByInstructor { get; set; } = null!;
    }

}
