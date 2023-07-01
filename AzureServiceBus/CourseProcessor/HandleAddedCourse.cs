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
        public void Run([ServiceBusTrigger("courseadded", "send-email", Connection = "connectionString")]string mySbMsg)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {mySbMsg}");
        }
    }
}
