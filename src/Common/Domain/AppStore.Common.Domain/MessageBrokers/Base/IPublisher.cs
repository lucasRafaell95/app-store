using System.Threading;

namespace AppStore.Common.Domain.MessageBrokers.Base
{
    /// <summary>
    /// Base interface for queue publishing
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IPublisher<TMessage>
    {
        /// <summary>
        /// Basic publishing method
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        void Publish(TMessage message, CancellationToken cancellationToken = default);
    }
}
