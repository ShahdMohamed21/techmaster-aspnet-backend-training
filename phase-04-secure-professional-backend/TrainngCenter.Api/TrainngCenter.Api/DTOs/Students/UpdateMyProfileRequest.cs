
using System.ComponentModel.DataAnnotations;

namespace TrainngCenter.Api.DTOs.Students
{
    public class UpdateMyProfileRequest
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        [StringLength(20)]
        public string? PhoneNumber { get; set; }
    }
}
