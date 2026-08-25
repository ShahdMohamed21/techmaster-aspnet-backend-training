using Microsoft.AspNetCore.Mvc;

namespace Drills.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestInfoController : ControllerBase
    {
        //Drill 13 - Header Reader Endpoint
        [HttpGet]
        public IActionResult GetRequestInfo()
        {
            var studentName = Request.Headers["X-Student-Name"].FirstOrDefault();

            if (string.IsNullOrWhiteSpace(studentName))
            {
                return BadRequest(new
                {
                    message = "X-Student-Name header is required"
                });
            }

            return Ok(new
            {
                studentName = studentName,
                path = Request.Path
            });
        }
    }
}
