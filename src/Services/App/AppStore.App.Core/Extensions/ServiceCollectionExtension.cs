using AppStore.App.Core.Caching;
using AppStore.App.Core.Caching.Contracts;
using AppStore.Common.Domain.DistributedCache.Base;
using AppStore.Common.Domain.DistributedCache.Redis;
using AppStore.Common.Infrastructure.DistributedCache;
using AppStore.Common.Infrastructure.DistributedCache.Base;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppStore.App.Core.Extensions
{
    public static class ServiceCollectionExtension
    {
        #region Public Methods

        public static IServiceCollection AddCoreDependencies(this IServiceCollection services, IConfiguration configuration)
            => services
                .AddMediator()
                .AddDistributedCache(configuration)
                .AddCachingServices();

        #endregion

        #region Private Methods

        private static IServiceCollection AddMediator(this IServiceCollection services)
            => services.AddMediatR(typeof(ServiceCollectionExtension).Assembly);

        private static IServiceCollection AddCachingServices(this IServiceCollection services)
            => services.AddScoped<IApplicationCacheService, ApplicationCacheService>();

        private static IServiceCollection AddDistributedCache(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("DistributedCache").Get<DistributedCacheSettings>();

            services
                .AddSingleton<IDistributedCacheSettings>(_ => settings)
                .AddScoped<IRedisCacheService, RedisCacheService<IDistributedCacheSettings>>();

            services.AddStackExchangeRedisCache(_ =>
            {
                _.Configuration = settings.ConnectionString;
            });

            return services;
        }

        #endregion
    }
}
