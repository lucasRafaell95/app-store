using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace AppStore.Common.Domain.Persistence.MongoDB
{
    /// <summary>
    /// Interface with mongoDB base methods
    /// </summary>
    public interface IMongoContext : IDisposable
    {
        /// <summary>
        /// Insert a new command into the currently open transaction
        /// </summary>
        /// <param name="command"></param>
        void AddCommand(Func<Task> command);

        /// <summary>
        /// Commit all pending commands
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Returns the collection using its name
        /// </summary>
        /// <typeparam name="TCollection"></typeparam>
        /// <param name="collectionName"></param>
        /// <returns></returns>
        IMongoCollection<TCollection> GetCollection<TCollection>(string collectionName);
    }
}
