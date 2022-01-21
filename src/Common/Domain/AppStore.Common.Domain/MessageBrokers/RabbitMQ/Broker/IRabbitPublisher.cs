using AppStore.Common.Domain.MessageBrokers.Base;
using System;

namespace AppStore.Common.Domain.MessageBrokers.RabbitMQ.Broker
{
    public interface IRabbitPublisher<TMessage> : IPublisher<TMessage>, IDisposable where TMessage : class { }
}
