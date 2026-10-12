
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TrainngCenter.Api.Services.Interfaces;

namespace TrainngCenter.Api.Controllers
{
    [ApiController]
    [Route("api/admin/activity-logs")]
    [Authorize(Roles = "Admin")]
    public class AuditLogsController : ControllerBase
    {
        private readonly IAuditLogService _service;

        public AuditLogsController(IAuditLogService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? userId,[FromQuery] string? entityName,[FromQuery] DateTime? from,[FromQuery] DateTime? to,[FromQuery] int pageNumber = 1,[FromQuery] int pageSize = 20)
        {
            if (pageNumber < 1 || pageSize < 1 || pageSize > 100)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Page number must be positive and page size must be between 1 and 100"
                });
            }

            if (from.HasValue && to.HasValue && from.Value > to.Value)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "'from' must be earlier than or equal to 'to'"
                });
            }

            var result = await _service.GetAllAsync( userId,entityName,from, to,pageNumber,pageSize);

            return Ok(new
            {
                success = true,
                data = result
            });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Audit log was not found"
                });
            }

            return Ok(new
            {
                success = true,
                data = result
            });
        }
    }
}
