using TrainingCenter.Api.Entities.Enrollments;
using TrainingCenter.Api.Entities.Instructors;

namespace TrainingCenter.Api.Entities.Tracks
{
    public class TrainingTrack
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int InstructorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Instructor Instructor { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
