using TrainingCenter.Api.Enums;

namespace TrainingCenter.Api.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public UserRole Role { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? LastLoginAt { get; set; }

        public int? StudentId { get; set; }

        public int? InstructorId { get; set; }
    }
}