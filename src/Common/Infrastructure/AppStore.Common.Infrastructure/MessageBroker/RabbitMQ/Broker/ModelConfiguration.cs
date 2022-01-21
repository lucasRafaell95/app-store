using AppStore.Common.Domain.MessageBrokers.RabbitMQ.Settings;
using RabbitMQ.Client;

namespace AppStore.Common.Infrastructure.MessageBroker.RabbitMQ.Broker
{
    public static class ModelConfiguration
    {
        #region Public Methods

        public static IModel ConfigureModel(IModel model, IRabbitSettings settings, bool bindConnection = true)
            => model is not null
                ? model
                : CreateModel(settings, bindConnection);

        #endregion

        #region Private Methods

        private static IModel CreateModel(IRabbitSettings settings, bool bindConnection)
        {
            var connection = ConfigureConnection(settings);

            var model = connection.CreateModel();

            return !bindConnection
                ? model
                : model.BindConnection(settings);
        }

        private static IConnection ConfigureConnection(IRabbitSettings settings)
            => new ConnectionFactory
            {
                UserName = settings.UserName,
                Password = settings.Password,
                HostName = settings.HostName,
                Port = settings.Port
            }.CreateConnection();

        private static IModel BindConnection(this IModel model, IRabbitSettings settings)
        {
            model.QueueBind(
                    settings.Queue,
                    settings.Exchange,
                    settings.RoutingKey
                );

            return model;
        }

        #endregion
    }
}