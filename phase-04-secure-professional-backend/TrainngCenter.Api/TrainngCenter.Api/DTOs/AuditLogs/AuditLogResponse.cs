
namespace TrainngCenter.Api.DTOs.AuditLogs
{
    public class AuditLogResponse
    {
        public int AuditLogId { get; set; }
        public int? UserId { get; set; }
        public string? UserRole { get; set; }
        public string Action { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public int? EntityId { get; set; }
        public string? Details { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? IpAddress { get; set; }
        public string? Metadata { get; set; }
    }
}
