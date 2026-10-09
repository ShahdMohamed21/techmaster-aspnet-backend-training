using TrainngCenter.Api.DTOs.AuditLogs;

namespace TrainngCenter.Api.Services.Interfaces
{
    public interface IAuditLogService
    {
        Task<List<AuditLogResponse>> GetAllAsync();

        Task<AuditLogResponse?> GetByIdAsync(int id);
    }
}
