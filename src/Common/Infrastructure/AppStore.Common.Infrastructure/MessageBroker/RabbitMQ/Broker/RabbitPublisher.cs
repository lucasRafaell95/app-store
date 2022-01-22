using AppStore.Common.Domain.MessageBrokers.RabbitMQ.Broker;
using AppStore.Common.Domain.MessageBrokers.RabbitMQ.Settings;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppStore.Common.Infrastructure.MessageBroker.RabbitMQ.Broker
{
    public sealed class RabbitPublisher<TMessage> : IRabbitPublisher<TMessage>, IDisposable where TMessage : class
    {
        #region Fields

        private IModel model;
        private readonly IRabbitSettings settings;
        private readonly ILogger logger;

        #endregion

        #region Constructor

        public RabbitPublisher(IRabbitSettings settings, ILogger<RabbitPublisher<TMessage>> logger)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region IRabbitPublisher Methods

        public void Publish(TMessage message, CancellationToken cancellationToken = default)
        {
            bool sucess = true;

            try
            {
                ConfigureModel();

                Task.Factory.StartNew(() =>
                {
                    var json = JsonConvert.SerializeObject(message);

                    var body = Encoding.UTF8.GetBytes(json);

                    Publish(body);
                }, cancellationToken);
            }
            catch (BrokerUnreachableException ex)
            {
                this.logger.LogError("Could not connect to broker", new
                {
                    ExceptionMessage = ex.Message,
                    Settings = new
                    {
                        Hostname = settings.HostName,
                        Port = settings.Port
                    }
                });
                sucess = false;
            }
            finally
            {
                this.logger.LogInformation("Finished message consumption", new { Sucess = sucess });

                CloseConnection();
            }
        }

        public void Dispose()
            => this.model?.Dispose();

        #endregion

        #region Private Methods

        private void Publish(byte[] body)
            => model.BasicPublish(settings.Exchange, settings.RoutingKey, null, body);

        private void ConfigureModel()
            => this.model = ModelConfiguration.ConfigureModel(this.model, settings, false);

        private void CloseConnection()
            => this.model?.Close();

        #endregion
    }
}
