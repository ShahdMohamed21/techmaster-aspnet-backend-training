namespace TrainngCenter.Api.DTOs.Instructor
{
    public class UpdateSessionRequest
    {
        public DateTime SessionDate { get; set; }

        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? MeetingLink { get; set; }

        public bool IsCompleted { get; set; }
    }
}