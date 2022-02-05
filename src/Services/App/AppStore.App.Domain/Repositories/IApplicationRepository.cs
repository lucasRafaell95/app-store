using AppStore.Common.Domain.Entities;
using AppStore.Common.Domain.Persistence.Base;
using System.Threading.Tasks;

namespace AppStore.App.Domain.Repositories
{
    /// <summary>
    /// App repository
    /// </summary>
    public interface IApplicationRepository : IRepository<Application>
    {
        /// <summary>
        /// Return a App using your name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<Application> GetApplicationByNameAsync(string name);
    }
}
