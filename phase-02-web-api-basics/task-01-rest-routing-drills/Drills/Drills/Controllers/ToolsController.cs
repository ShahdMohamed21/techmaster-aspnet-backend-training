using Microsoft.AspNetCore.Mvc;

namespace Drills.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToolsController : Controller
    {
        //Drill 02 - Route Parameter Echo
        [HttpGet("echo/{name}")]
        public IActionResult Greeting(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                return BadRequest(new
                {
                    Message = "Name Should Not Be Null"
                });
            }
            return Ok(new
            {
                OriginalName = name,
                Message = $"Welcome {name}"

            });
        }
    }
}
