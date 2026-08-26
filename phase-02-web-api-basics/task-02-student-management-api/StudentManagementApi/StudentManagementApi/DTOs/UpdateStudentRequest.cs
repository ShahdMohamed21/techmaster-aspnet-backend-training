using System.ComponentModel.DataAnnotations;

namespace StudentManagementApi.DTOs
{
    public class UpdateStudentRequest
    {
        [Required]
        public string FullName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string TrackName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public DateTime EnrollmentDate { get; set; }

        public bool IsActive { get; set; }
        public string? GitHubProfileUrl { get; set; }
        public string? LinkedInProfileUrl { get; set; }
    }
}
