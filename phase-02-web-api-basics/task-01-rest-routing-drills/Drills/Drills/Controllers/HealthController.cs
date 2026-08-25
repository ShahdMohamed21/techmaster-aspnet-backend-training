using Microsoft.AspNetCore.Mvc;

namespace Drills.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : Controller
    {
        // Drill 01 - Health Check Endpoint
        [HttpGet]
        public IActionResult GetHealth() {
            return Ok(new {
                Status = "Running",
                ervice = "TechMaster API",
                time = DateTime.UtcNow
            });
        }
       




    }
}
