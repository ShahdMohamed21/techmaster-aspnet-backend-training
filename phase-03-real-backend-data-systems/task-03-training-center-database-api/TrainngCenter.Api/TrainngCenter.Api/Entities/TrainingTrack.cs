namespace TrainingCenter.Api.Entities
{
    public class TrainingTrack
    {
        public int TrainingTrackId { get; set; }

        public string Title { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public string Level { get; set; } = null!;

        public int Capacity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; } = null!;

        public int InstructorId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        // Navigation Properties

        public Instructor Instructor { get; set; } = null!;

        public ICollection<Enrollment> Enrollments { get; set; }
          
    }
}