using AppStore.App.Domain.Repositories;
using AppStore.App.Persistance.Repositories;
using AppStore.Common.Domain.Persistence.MongoDB.Base;
using AppStore.Common.Infrastructure.Persistance.MongoDB.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppStore.App.Persistance.Extensions
{
    public static class ServiceCollectionExtension
    {
        #region Public Methods

        public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
            => services
            .AddMongoDB(configuration)
            .AddRepositories();

        #endregion

        #region Private Methods

        private static IServiceCollection AddRepositories(this IServiceCollection services)
            => services.AddScoped<IApplicationRepository, ApplicationRepository<IMongoSettings>>();

        private static IServiceCollection AddMongoDB(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IMongoSettings>(_ => configuration.GetSection("MongoDB").Get<MongoSettings>());

            return services;
        }

        #endregion
    }
}
