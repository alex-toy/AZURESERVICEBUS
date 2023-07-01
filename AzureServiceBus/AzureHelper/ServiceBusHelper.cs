using Azure.Identity;
using Azure.Messaging.ServiceBus;

namespace AzureHelper
{
    public class ServiceBusHelper : IServiceBusHelper
    {
        private readonly string _queueName;
        private ServiceBusSender _sender;
        private readonly ServiceBusClient _client;

        public ServiceBusHelper(string serviceBusUrl, string queueName)
        {
            _queueName = queueName;
            _client = new ServiceBusClient(serviceBusUrl, new DefaultAzureCredential());
        }

        public async Task SendMessage(string body, Dictionary<string, string> properties = null, int TTL = 30, bool scheduled = false)
        {
            if (_sender == null) _sender = _client.CreateSender(_queueName);

            ServiceBusMessage message = new ServiceBusMessage(body);
            if (scheduled) message.ScheduledEnqueueTime = DateTimeOffset.UtcNow.AddSeconds(5);
            message.ContentType = "application/json";
            message.TimeToLive = TimeSpan.FromSeconds(TTL);
            AddProperties(properties, message);
            await _sender.SendMessageAsync(message);
        }

        private static void AddProperties(Dictionary<string, string> properties, ServiceBusMessage message)
        {
            if (properties != null)
            {
                foreach (var property in properties) message.ApplicationProperties.Add(property.Key, property.Value);
            }
        }
    }
}