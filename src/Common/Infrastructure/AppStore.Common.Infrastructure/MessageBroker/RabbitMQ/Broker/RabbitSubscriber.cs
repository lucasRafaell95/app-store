using AppStore.Common.Domain.MessageBrokers.RabbitMQ.Broker;
using AppStore.Common.Domain.MessageBrokers.RabbitMQ.Settings;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AppStore.Common.Infrastructure.MessageBroker.RabbitMQ.Broker
{
    public sealed class RabbitSubscriber<TMessage> : IRabbitSubscriber<TMessage> where TMessage : class
    {
        #region Fields

        private IModel model;
        private readonly IRabbitSettings settings;
        private readonly ILogger logger;

        #endregion

        #region Constructor
        public RabbitSubscriber(IRabbitSettings settings,
                                ILogger<RabbitSubscriber<TMessage>> logger)
        {
            this.settings = settings ?? throw new ArgumentException(nameof(settings));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #endregion

        #region IRabbitSubscriber Methods

        public void Subscribe(Action<TMessage> action)
        {
            bool sucess = true;

            try
            {
                ConfigureModel();

                Task.Factory.StartNew(() =>
                {
                    Consume(action);
                });
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

        private void Consume(Action<TMessage> action)
        {
            var consumer = new AsyncEventingBasicConsumer(this.model);

            consumer.Received += async (sender, args) =>
            {
                var body = args.Body.ToArray();

                var content = Encoding.UTF8.GetString(body);

                var message = JsonConvert.DeserializeObject<TMessage>(content);

                action(message);
            };

            this.model.BasicConsume(queue: settings.Queue, consumer: consumer);
        }

        private void ConfigureModel()
            => this.model = ModelConfiguration.ConfigureModel(this.model, settings);

        private void CloseConnection()
            => this.model?.Close();

        #endregion
    }
}
