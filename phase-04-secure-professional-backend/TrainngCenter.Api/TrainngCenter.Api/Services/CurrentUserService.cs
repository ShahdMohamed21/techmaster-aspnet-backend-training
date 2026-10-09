using System.Security.Claims;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService( IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var value = _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirstValue(
                        ClaimTypes.NameIdentifier);

                return int.TryParse(value, out var userId)? userId: 0;
            }
        }

        public int? StudentId
        {
            get
            {
                var value = _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirstValue("StudentId");

                return int.TryParse(value, out var studentId) ? studentId: null;
            }
        }

        public int? InstructorId
        {
            get
            {
                var value = _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirstValue("InstructorId");

                return int.TryParse(value, out var instructorId)? instructorId: null;
            }
        }

        public string? Role
        {
            get
            {
                return _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirstValue(ClaimTypes.Role);
            }
        }
    }
}