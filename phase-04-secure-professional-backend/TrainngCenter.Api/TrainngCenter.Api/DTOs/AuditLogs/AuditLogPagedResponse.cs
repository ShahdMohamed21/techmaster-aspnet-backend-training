
namespace TrainngCenter.Api.DTOs.AuditLogs
{
    public class AuditLogPagedResponse
    {
        public List<AuditLogResponse> Data { get; set; } = new();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }
}
