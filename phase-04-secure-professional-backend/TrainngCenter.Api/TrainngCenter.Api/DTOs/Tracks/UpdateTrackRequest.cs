using System.ComponentModel.DataAnnotations;

namespace TrainngCenter.Api.DTOs.Tracks
{
    public class UpdateTrackRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Level { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int Capacity { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int InstructorId { get; set; }
    }
}
