using Microsoft.AspNetCore.Mvc;

namespace RateLimitingDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private List<WeatherForecast> _weatherForecasts = new()
        {
                new()
                {
                    Id  =1,
                    Date = DateTime.Now.AddDays(new Random().Next(1,10)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                },
                new()
                {
                    Id=2,
                    Date = DateTime.Now.AddDays(new Random().Next(1,10)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                },
                new()
                {
                    Id  =   3,
                    Date = DateTime.Now.AddDays(new Random().Next(1,10)),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                },
        };


        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return _weatherForecasts.ToArray();
        }

        [HttpPost(Name = "PostWeatherForecast")]
        public IActionResult Post()
        {
            
            return Ok("Data save successfully");
        }
    }
}