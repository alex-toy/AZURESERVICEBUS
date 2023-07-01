using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CourseProcessor
{
    public class HandleAddedCourse
    {
        private readonly ILogger<HandleAddedCourse> _logger;

        public HandleAddedCourse(ILogger<HandleAddedCourse> log)
        {
            _logger = log;
        }

        [FunctionName("HandleAddedCourse")]
        public void Run([ServiceBusTrigger("courseadded", "send-email", Connection = "connectionString")] string addedCourseEvent)
        {
            if (addedCourseEvent.Contains("2020")) throw new Exception("Cannot process year 2020");
            _logger.LogInformation($"Send email: {addedCourseEvent}");
        }

        [FunctionName("UpdatingReport")]
        public void UpdatingReport([ServiceBusTrigger("courseadded", "update-report", Connection = "connectionString")] string addedCourseEvent)
        {
            _logger.LogInformation($"Update report: {addedCourseEvent}");
        }
    }
}
