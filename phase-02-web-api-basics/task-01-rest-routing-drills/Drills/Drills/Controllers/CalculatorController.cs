using Microsoft.AspNetCore.Mvc;

namespace Drills.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculatorController : Controller
    {
        // Drill 03 - Query String Calculator

        [HttpGet("Add")]
        public IActionResult Add([FromQuery] decimal a, [FromQuery] decimal b)
        {
            return Ok(new
            {
                A = a,
                B = b,
                Operation = "addition",
                Result = a + b
            });
        }
    }
}
