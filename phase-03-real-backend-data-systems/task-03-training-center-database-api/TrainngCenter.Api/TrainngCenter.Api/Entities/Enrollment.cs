namespace TrainingCenter.Api.Entities
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public int StudentId { get; set; }

        public int TrainingTrackId { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        public string Status { get; set; } = null!;

        public decimal ProgressPercentage { get; set; }

        public string? FinalResult { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        // Navigation Properties

        public Student Student { get; set; } = null!;

        public TrainingTrack TrainingTrack { get; set; } = null!;

        public ICollection<Payment> Payments { get; set; }
           
    }
}