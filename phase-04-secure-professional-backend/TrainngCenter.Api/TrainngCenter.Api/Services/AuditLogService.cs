using Microsoft.EntityFrameworkCore;
using TrainingCenter.Api.Data;
using TrainngCenter.Api.DTOs.AuditLogs;
using TrainngCenter.Api.Services.Interfaces;


namespace TrainngCenter.Api.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly ApplicationDbContext _context;

        public AuditLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuditLogResponse>> GetAllAsync()
        {
            return await _context.AuditLogs
                .AsNoTracking()
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new AuditLogResponse
                {
                    AuditLogId = a.AuditLogId,
                    UserId = a.UserId,
                    Action = a.Action,
                    EntityName = a.EntityName,
                    EntityId = a.EntityId,
                    Details = a.Details,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();
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
                    Action = a.Action,
                    EntityName = a.EntityName,
                    EntityId = a.EntityId,
                    Details = a.Details,
                    CreatedAt = a.CreatedAt
                })
                .FirstOrDefaultAsync();
        }
    }
}

