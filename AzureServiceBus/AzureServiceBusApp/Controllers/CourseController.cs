using AzureHelper;
using AzureServiceBusApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace AzureServiceBusApp.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IServiceBusHelper _serviceBus;

        public CourseController(ILogger<WeatherForecastController> logger, IServiceBusHelper serviceBus)
        {
            _logger = logger;
            _serviceBus = serviceBus;
        }

        [HttpPost]
        public async Task AddWeatherForecast(Course course)
        {
            // add course to a database

            var courseAdded = new CourseAdded()
            {
                Id = Guid.NewGuid(),
                CreatedDateTime = DateTime.Now,
                ForDate = course.Date,
            };

            string? body = JsonSerializer.Serialize(course);

            await _serviceBus.SendMessage(body, null, 3000);
        }
    }
}
