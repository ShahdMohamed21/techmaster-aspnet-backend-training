using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TrainingCenter.Api.Data;
using TrainingCenter.Api.DTOs.Auth;
using TrainingCenter.Api.Entities;
using TrainingCenter.Api.Enums;
using TrainingCenter.Api.Services.Interfaces;

namespace TrainingCenter.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;

        public AuthService(
            ApplicationDbContext context,
            IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _passwordHasher = new PasswordHasher<ApplicationUser>();
        }

        public async Task<(bool Success, string Message, AuthResponse? Data)> RegisterAsync(
            RegisterRequest request)
        {
            if (!request.Password.Equals(
                    request.ConfirmPassword,
                    StringComparison.Ordinal))
            {
                return (false, "Passwords do not match", null);
            }

            if (!Enum.TryParse<UserRole>(
                    request.Role,
                    true,
                    out var role))
            {
                return (false, "Invalid role", null);
            }

            if (role == UserRole.Admin)
            {
                return (
                    false,
                    "Admin registration is not allowed",
                    null);
            }

            var email = request.Email.Trim().ToLowerInvariant();

            var exists = await _context.ApplicationUsers
                .AnyAsync(x => x.Email == email);

            if (exists)
            {
                return (
                    false,
                    "Email is already registered",
                    null);
            }

            await using var transaction =
                await _context.Database.BeginTransactionAsync();

            try
            {
                var user = new ApplicationUser
                {
                    FullName = request.FullName.Trim(),
                    Email = email,
                    Role = role,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                user.PasswordHash =
                    _passwordHasher.HashPassword(
                        user,
                        request.Password);

                _context.ApplicationUsers.Add(user);

                await _context.SaveChangesAsync();

                if (role == UserRole.Student)
                {
                    var student = new Student
                    {
                        FullName = user.FullName,
                        Email = user.Email,
                        PhoneNumber = null,
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true,
                        IsDeleted = false
                    };

                    _context.Students.Add(student);

                    await _context.SaveChangesAsync();

                    user.StudentId = student.StudentId;

                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return (
                    true,
                    "User registered successfully",
                    new AuthResponse
                    {
                        UserId = user.Id,
                        FullName = user.FullName,
                        Email = user.Email,
                        Role = user.Role.ToString()
                    });
            }
            catch
            {
                await transaction.RollbackAsync();

                return (
                    false,
                    "Registration failed",
                    null);
            }
        }

        public async Task<(bool Success, string Message, AuthResponse? Data)> LoginAsync(
            LoginRequest request)
        {
            var email = request.Email.Trim().ToLowerInvariant();

            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
            {
                return (
                    false,
                    "Invalid email or password",
                    null);
            }

            if (!user.IsActive)
            {
                return (
                    false,
                    "User account is inactive",
                    null);
            }

            var verification =
                _passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    request.Password);

            if (verification == PasswordVerificationResult.Failed)
            {
                return (
                    false,
                    "Invalid email or password",
                    null);
            }

            user.LastLoginAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            var accessToken = GenerateAccessToken(user);

            var refreshToken = GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                UserId = user.Id,
                TokenHash = HashToken(refreshToken),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(
                    GetRefreshTokenDays())
            };

            _context.RefreshTokens.Add(refreshTokenEntity);

            await _context.SaveChangesAsync();

            return (
                true,
                "Login successful",
                new AuthResponse
                {
                    AccessToken = accessToken.Token,
                    RefreshToken = refreshToken,
                    ExpiresAt = accessToken.ExpiresAt,
                    UserId = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Role = user.Role.ToString()
                });
        }

        public async Task<CurrentUserResponse?> GetCurrentUserAsync(
            ClaimsPrincipal principal)
        {
            var userId = GetUserId(principal);

            if (userId == null)
                return null;

            var user = await _context.ApplicationUsers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return null;

            return new CurrentUserResponse
            {
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role.ToString(),
                LinkedStudentId = user.StudentId,
                LinkedInstructorId = user.InstructorId
            };
        }

        public async Task<(bool Success, string Message)> ChangePasswordAsync(
            ClaimsPrincipal principal,
            ChangePasswordRequest request)
        {
            var userId = GetUserId(principal);

            if (userId == null)
                return (false, "Invalid user");

            if (!request.NewPassword.Equals(
                    request.ConfirmNewPassword,
                    StringComparison.Ordinal))
            {
                return (
                    false,
                    "Passwords do not match");
            }

            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(x => x.Id == userId);

            if (user == null)
                return (
                    false,
                    "User not found");

            var verification =
                _passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    request.CurrentPassword);

            if (verification == PasswordVerificationResult.Failed)
            {
                return (
                    false,
                    "Current password is incorrect");
            }

            user.PasswordHash =
                _passwordHasher.HashPassword(
                    user,
                    request.NewPassword);

            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return (
                true,
                "Password changed successfully");
        }

        public async Task<(bool Success, string Message, AuthResponse? Data)> RefreshTokenAsync(
            RefreshTokenRequest request)
        {
            var tokenHash = HashToken(request.RefreshToken);

            var storedToken = await _context.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(
                    x => x.TokenHash == tokenHash);

            if (storedToken == null)
            {
                return (
                    false,
                    "Invalid refresh token",
                    null);
            }

            if (storedToken.RevokedAt != null)
            {
                return (
                    false,
                    "Refresh token has been revoked",
                    null);
            }

            if (storedToken.ExpiresAt <= DateTime.UtcNow)
            {
                return (
                    false,
                    "Refresh token has expired",
                    null);
            }

            if (!storedToken.User.IsActive)
            {
                return (
                    false,
                    "User account is inactive",
                    null);
            }

            storedToken.RevokedAt = DateTime.UtcNow;

            var accessToken =
                GenerateAccessToken(storedToken.User);

            var newRefreshToken =
                GenerateRefreshToken();

            _context.RefreshTokens.Add(
                new RefreshToken
                {
                    UserId = storedToken.UserId,
                    TokenHash = HashToken(newRefreshToken),
                    CreatedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddDays(
                        GetRefreshTokenDays())
                });

            await _context.SaveChangesAsync();

            return (
                true,
                "Token refreshed successfully",
                new AuthResponse
                {
                    AccessToken = accessToken.Token,
                    RefreshToken = newRefreshToken,
                    ExpiresAt = accessToken.ExpiresAt,
                    UserId = storedToken.User.Id,
                    FullName = storedToken.User.FullName,
                    Email = storedToken.User.Email,
                    Role = storedToken.User.Role.ToString()
                });
        }

        public async Task<(bool Success, string Message)> LogoutAsync(
            ClaimsPrincipal principal,
            LogoutRequest request)
        {
            var userId = GetUserId(principal);

            if (userId == null)
                return (
                    false,
                    "Invalid user");

            var tokenHash =
                HashToken(request.RefreshToken);

            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(
                    x => x.UserId == userId &&
                         x.TokenHash == tokenHash);

            if (token == null)
            {
                return (
                    false,
                    "Refresh token not found");
            }

            token.RevokedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return (
                true,
                "Logged out successfully");
        }

        private (string Token, DateTime ExpiresAt) GenerateAccessToken(
            ApplicationUser user)
        {
            var key = _configuration["Jwt:Key"];

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidOperationException(
                    "JWT key is not configured");
            }

            var expiresAt =
                DateTime.UtcNow.AddMinutes(
                    GetAccessTokenMinutes());

            var claims = new List<Claim>
            {
                new Claim(
                    JwtRegisteredClaimNames.Sub,
                    user.Id.ToString()),

                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id.ToString()),

                new Claim(
                    JwtRegisteredClaimNames.Email,
                    user.Email),

                new Claim(
                    ClaimTypes.Email,
                    user.Email),

                new Claim(
                    ClaimTypes.Role,
                    user.Role.ToString()),

                new Claim(
                    JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            };

            if (user.StudentId.HasValue)
            {
                claims.Add(
                    new Claim(
                        "StudentId",
                        user.StudentId.Value.ToString()));
            }

            if (user.InstructorId.HasValue)
            {
                claims.Add(
                    new Claim(
                        "InstructorId",
                        user.InstructorId.Value.ToString()));
            }

            var securityKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(key));

            var credentials =
                new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials);

            return (
                new JwtSecurityTokenHandler()
                    .WriteToken(token),
                expiresAt);
        }

        private static string GenerateRefreshToken()
        {
            var bytes =
                RandomNumberGenerator.GetBytes(64);

            return Convert.ToBase64String(bytes);
        }

        private static string HashToken(string token)
        {
            var bytes =
                SHA256.HashData(
                    Encoding.UTF8.GetBytes(token));

            return Convert.ToHexString(bytes);
        }

        private static int GetUserId(
            ClaimsPrincipal principal)
        {
            var claim =
                principal.FindFirst(
                    ClaimTypes.NameIdentifier)
                ?? principal.FindFirst(
                    JwtRegisteredClaimNames.Sub);

            return claim != null &&
                   int.TryParse(
                       claim.Value,
                       out var id)
                ? id
                : 0;
        }

        private int GetAccessTokenMinutes()
        {
            return _configuration.GetValue<int>(
                "Jwt:AccessTokenMinutes",
                30);
        }

        private int GetRefreshTokenDays()
        {
            return _configuration.GetValue<int>(
                "Jwt:RefreshTokenDays",
                7);
        }
    }
}