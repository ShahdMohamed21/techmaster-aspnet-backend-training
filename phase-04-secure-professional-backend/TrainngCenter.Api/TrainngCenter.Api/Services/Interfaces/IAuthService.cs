using System.Security.Claims;
using TrainingCenter.Api.DTOs.Auth;

namespace TrainingCenter.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(bool Success, string Message, AuthResponse? Data)> RegisterAsync(RegisterRequest request);

        Task<(bool Success, string Message, AuthResponse? Data)> LoginAsync(LoginRequest request);

        Task<CurrentUserResponse?> GetCurrentUserAsync(ClaimsPrincipal principal);

        Task<(bool Success, string Message)> ChangePasswordAsync(ClaimsPrincipal principal,ChangePasswordRequest request);

        Task<(bool Success, string Message, AuthResponse? Data)> RefreshTokenAsync(RefreshTokenRequest request);

        Task<(bool Success, string Message)> LogoutAsync(ClaimsPrincipal principal, LogoutRequest request);
    }
}