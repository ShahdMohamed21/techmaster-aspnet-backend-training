namespace TrainingCenter.Api.Entities
{
    public class Instructor
    {
        public int InstructorId { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Specialization { get; set; } = null!;

        public string? Bio { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<TrainingTrack> TrainingTracks { get; set; }
           
    }
}