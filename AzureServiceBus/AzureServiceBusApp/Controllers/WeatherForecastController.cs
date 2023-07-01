using AzureHelper;
using AzureServiceBusApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AzureServiceBusApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IServiceBusHelper _serviceBus;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IServiceBusHelper serviceBusHelper)
        {
            _logger = logger;
            _serviceBus = serviceBusHelper;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public async Task AddWeatherForecast(WeatherForecast weatherForecast)
        {
            string? body = JsonSerializer.Serialize(weatherForecast);

            await _serviceBus.SendMessage(body, null, 3000, weatherForecast.Scheduled);
        }
    }
}