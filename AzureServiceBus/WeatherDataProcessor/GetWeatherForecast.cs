using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace WeatherDataProcessor
{
    public class GetWeatherForecast
    {
        [FunctionName("GetWeatherForecast")]
        public void Run([ServiceBusTrigger("add-weather-data", Connection = "connectionString")]string weatherData, ILogger log)
        {
            if (weatherData.Length > 30) throw new Exception();
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {weatherData}");
        }
    }
}
