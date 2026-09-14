using TrainingCenter.Api.Entities.Tracks;

namespace TrainingCenter.Api.Entities.Instructors
{
    public class Instructor
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public ICollection<TrainingTrack> TrainingTracks { get; set; }
           
    }
}
