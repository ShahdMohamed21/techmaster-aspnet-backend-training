
using System.Security.Claims;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainngCenter.Api.DTOs.AuditLogs;
using TrainngCenter.Api.Entities;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditLogService( ApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LogAsync(string action,string entityName,int? entityId,string description,int? userId = null,string? userRole = null, object? metadata = null)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var claims = httpContext?.User;

            var userIdClaim =
                claims?.FindFirst(ClaimTypes.NameIdentifier)?.Value?? claims?.FindFirst("sub")?.Value;

            var roleClaim =claims?.FindFirst(ClaimTypes.Role)?.Value?? claims?.FindFirst("role")?.Value;

            var effectiveUserId = userId?? (int.TryParse(userIdClaim, out var parsedId)? parsedId: null);

            var effectiveUserRole = userRole ?? roleClaim;

            var log = new AuditLog
            {
                UserId = effectiveUserId,
                UserRole = effectiveUserRole,
                Action = action,
                EntityName = entityName,
                EntityId = entityId,
                Description = description,
                Details = description,
                CreatedAt = DateTime.UtcNow,
                IpAddress = httpContext?.Connection.RemoteIpAddress?.ToString(),
                Metadata = JsonSerializer.Serialize(new
                {
                    RequestId = httpContext?.TraceIdentifier,
                    Data = metadata
                })
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<AuditLogPagedResponse> GetAllAsync(int? userId,string? entityName,DateTime? from,DateTime? to,int pageNumber,int pageSize)
        {
            var query = _context.AuditLogs.AsNoTracking().AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(a => a.UserId == userId.Value);
            }

            if (!string.IsNullOrWhiteSpace(entityName))
            {
                query = query.Where(a => a.EntityName == entityName);
            }

            if (from.HasValue)
            {
                query = query.Where(a => a.CreatedAt >= from.Value);
            }

            if (to.HasValue)
            {
                query = query.Where(a => a.CreatedAt <= to.Value);
            }

            var totalCount = await query.CountAsync();

            var logs = await query
                .OrderByDescending(a => a.CreatedAt)
                .ThenByDescending(a => a.AuditLogId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AuditLogResponse
                {
                    AuditLogId = a.AuditLogId,
                    UserId = a.UserId,
                    UserRole = a.UserRole,
                    Action = a.Action,
                    EntityName = a.EntityName,
                    EntityId = a.EntityId,
                    Details = a.Details,
                    Description = a.Description,
                    CreatedAt = a.CreatedAt,
                    IpAddress = a.IpAddress,
                    Metadata = a.Metadata
                })
                .ToListAsync();

            return new AuditLogPagedResponse
            {
                Data = logs,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(
                    totalCount / (double)pageSize)
            };
        }

        public async Task<AuditLogResponse?> GetByIdAsync(int id)
        {
            return await _context.AuditLogs
                .AsNoTracking()
                .Where(a => a.AuditLogId == id)
                .Select(a => new AuditLogResponse
                {
                    AuditLogId = a.AuditLogId,
                    UserId = a.UserId,
                    UserRole = a.UserRole,
                    Action = a.Action,
                    EntityName = a.EntityName,
                    EntityId = a.EntityId,
                    Details = a.Details,
                    Description = a.Description,
                    CreatedAt = a.CreatedAt,
                    IpAddress = a.IpAddress,
                    Metadata = a.Metadata
                })
                .FirstOrDefaultAsync();
        }
    }
}
