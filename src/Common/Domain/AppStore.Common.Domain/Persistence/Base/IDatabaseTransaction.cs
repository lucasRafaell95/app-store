using System;
using System.Threading.Tasks;

namespace AppStore.Common.Domain.Persistence.Base
{
    /// <summary>
    /// Database transaction control interface
    /// </summary>
    public interface IDatabaseTransaction : IDisposable
    {
        /// <summary>
        /// Commit all changes not yet effective to the database
        /// </summary>
        /// <returns></returns>
        Task<bool> CommitTransaction();
    }
}
