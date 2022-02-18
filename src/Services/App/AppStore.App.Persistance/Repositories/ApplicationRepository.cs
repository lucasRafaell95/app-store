using AppStore.App.Domain.Repositories;
using AppStore.Common.Domain.Entities;
using AppStore.Common.Domain.Persistence.MongoDB.Base;
using AppStore.Common.Infrastructure.Persistance.Base;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace AppStore.App.Persistance.Repositories
{
    public sealed class ApplicationRepository<TMongoSettings> : Repository<Application>, IApplicationRepository where TMongoSettings : IMongoSettings
    {
        #region Constructor

        public ApplicationRepository(TMongoSettings settings) : base(settings) { }

        #endregion

        #region IApplicationRepository Methods

        public async Task<Application> GetApplicationByNameAsync(string name)
        {
            var result = await collection.FindAsync(Builders<Application>.Filter.Eq("Name", name));

            return result.FirstOrDefault();
        }

        #endregion
    }
}
