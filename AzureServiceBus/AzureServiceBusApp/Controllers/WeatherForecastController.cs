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
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
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
            string connectionString = "Endpoint=sb://weatherforecastsb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=zknamkoJKsWSh5JqfZXyJYNWPm2vgqt60+ASbM1Ok34=";
            string queue = "add-weather-data";

            string? body = JsonSerializer.Serialize(weatherForecast);

            var serviceBus = new ServiceBusHelper(connectionString, queue);
            await serviceBus.SendMessage(body);
        }
    }
}