using Drills.Services;
using Microsoft.AspNetCore.Mvc;

namespace Drills.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    
    public class ConverterController : Controller
    {
        private readonly ConverterService _convertor;
        public ConverterController(ConverterService convertor)
        {
            _convertor = convertor;
        }
        //Drill 04 - Temperature Conversion API
        [HttpGet("celsius-to-fahrenheit")]
        public IActionResult ConvertTemp([FromQuery] decimal value) {
            var fahrenheit = _convertor.ConvertCelsiusToFahrenheit(value);
            return Ok(new {
                celsius = value,
                fahrenheit = fahrenheit,
                formulaUsed = "(C × 9/5) + 32" });
        }
    }
}
            
