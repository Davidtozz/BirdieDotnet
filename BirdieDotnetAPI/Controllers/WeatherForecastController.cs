using Microsoft.AspNetCore.Mvc;

namespace BirdieDotnetAPI.Controllers
{
    [ApiController]
    [Route("[controller]")] // [controller] is transformed into "/weatherforecast" route
    public class WeatherForecastController : ControllerBase
    {
        
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "Hello World!";
        }
    }
}