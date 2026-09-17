using TrainingCenter.Api.Entities;

namespace TrainngCenter.Api.DTOs.Tracks
{
    public class CreateTrackRequest
    { 
        public string Title { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        public string Level { get; set; } = null!;

        public int Capacity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; } = null!;

        public int InstructorId { get; set; }


    }
}
