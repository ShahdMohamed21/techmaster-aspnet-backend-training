
using System.ComponentModel.DataAnnotations;

namespace TrainngCenter.Api.DTOs.Instructor
{
    public class CreateSessionRequest
    {
        [Required]
        public DateTime? SessionDate { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Url]
        public string? MeetingLink { get; set; }
    }
}
