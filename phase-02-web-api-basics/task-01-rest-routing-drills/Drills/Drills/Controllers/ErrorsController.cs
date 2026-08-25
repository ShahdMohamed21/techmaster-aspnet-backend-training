using Microsoft.AspNetCore.Mvc;

namespace Drills.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ErrorsController : ControllerBase
    {
        //Drill 15 - Standard Error Shape

        [HttpGet("demo")]
        public IActionResult Demo(string type)
        {
            if (type == "bad-request")
            {
                return BadRequest(new
                {
                    message = "Invalid request",
                    code = "BAD_REQUEST",
                    details = new[]
                    {
                        "The request data is invalid"
                    }
                });
            }

            if (type == "not-found")
            {
                return NotFound(new
                {
                    message = "Resource not found",
                    code = "NOT_FOUND",
                    details = new[]
                    {
                        "The requested resource does not exist"
                    }
                });
            }

            return BadRequest(new
            {
                message = "Invalid error type",
                code = "INVALID_TYPE",
                details = new[]
                {
                    "Use 'bad-request' or 'not-found"
                }
            });
        }
    }
}