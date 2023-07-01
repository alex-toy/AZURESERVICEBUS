
namespace AzureHelper
{
    public interface IServiceBusHelper
    {
        Task SendMessage(string body, Dictionary<string, string> properties = null, int TTL = 30, bool scheduled = false);
    }
}