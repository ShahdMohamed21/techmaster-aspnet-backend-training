using System.ComponentModel.DataAnnotations;

namespace TrainingCenter.Api.DTOs.Auth
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
}