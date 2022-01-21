using AppStore.Common.Domain.MessageBrokers.RabbitMQ.Settings;

namespace AppStore.Common.Infrastructure.MessageBroker.RabbitMQ.Settings
{
    public sealed class RabbitSettings : IRabbitSettings
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string Exchange { get; set; }
        public string VirtualHost { get; set; }
        public string Queue { get; set; }
        public string RoutingKey { get; set; }
    }
}
