using AppStore.Common.Domain.MessageBrokers.Base;
using System;

namespace AppStore.Common.Domain.MessageBrokers.RabbitMQ.Broker
{
    public interface IRabbitSubscriber<TMessage> : ISubscriber<TMessage>, IDisposable where TMessage : class { }
}
