using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;

namespace Drills.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradesController : Controller
    {
        //Drill 05 - Grade API
        [HttpGet("calculate")]
        public IActionResult Calculate([FromQuery]decimal score) {
            if (score < 0 || score > 100)
            {
                return BadRequest(new
                {
                    error = "Score must be between 0 and 100"
                }); 
            }
            if(score >= 50 && score<65) {
                return Ok(new
                {
                    score = score,
                    status = "Pass",
                    Grade="B-"
                });
            }
            else if(score >= 65 && score <= 85)
            {
                return Ok(new
                {
                    score = score,
                    status = "Pass",
                    Grade = "B"

                });
            }
            else if (score > 85&& score <= 100)
            {
                return Ok(new
                {
                    score = score,
                    status = "Pass",
                    Grade = "A"

                });
            }
            else
            {
                return Ok(new
                {
                    score = score,
                    status = "Fail",
                    Grade = "F"

                });
            }



        }
    }
}
