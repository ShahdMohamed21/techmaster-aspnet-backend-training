
using TrainngCenter.Api.DTOs.AuditLogs;

namespace TrainngCenter.Api.Services.Interfaces
{
    public interface IAuditLogService
    {
        Task LogAsync(string action,string entityName,int? entityId,string description,int? userId = null,string? userRole = null,object? metadata = null);

        Task<AuditLogPagedResponse> GetAllAsync(int? userId,string? entityName,DateTime? from,DateTime? to,int pageNumber,int pageSize);

        Task<AuditLogResponse?> GetByIdAsync(int id);
    }
}
