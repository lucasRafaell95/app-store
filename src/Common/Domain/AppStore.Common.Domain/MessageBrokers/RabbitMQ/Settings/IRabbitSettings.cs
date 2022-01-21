namespace AppStore.Common.Domain.MessageBrokers.RabbitMQ.Settings
{
    public interface IRabbitSettings
    {
        string UserName { get; set; }
        string Password { get; set; }
        string HostName { get; set; }
        int Port { get; set; }
        string Exchange { get; set; }
        string VirtualHost { get; set; }
        string Queue { get; set; }
        string RoutingKey { get; set; }
    }
}
