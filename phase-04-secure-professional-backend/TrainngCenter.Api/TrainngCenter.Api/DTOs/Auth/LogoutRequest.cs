using System.ComponentModel.DataAnnotations;

namespace TrainingCenter.Api.DTOs.Auth
{
    public class LogoutRequest
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}