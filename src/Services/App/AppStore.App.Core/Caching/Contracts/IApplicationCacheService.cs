using AppStore.Common.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AppStore.App.Core.Caching.Contracts
{
    /// <summary>
    /// Application Redis Cache interface
    /// </summary>
    public interface IApplicationCacheService
    {
        /// <summary>
        /// Return a list of Application
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="methodName"></param>
        /// <returns></returns>
        Task<List<Application>> GetApplicationsAsync(Func<Task<List<Application>>> dataSource, [CallerMemberName] string methodName = null);
    }
}
