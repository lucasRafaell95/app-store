using System;

namespace AppStore.Common.Domain.MessageBrokers.Base
{
    /// <summary>
    /// Default interface for queue subscribing
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface ISubscriber<TMessage>
    {
        /// <summary>
        /// Default method for subscribing to queues
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        void Subscribe(Action<TMessage> action);
    }
}
