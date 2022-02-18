using AppStore.App.Core.Caching;
using AppStore.App.Core.Caching.Contracts;
using AppStore.App.Core.Tools.Pipelines;
using AppStore.Common.Domain.DistributedCache.Base;
using AppStore.Common.Domain.DistributedCache.Redis;
using AppStore.Common.Infrastructure.DistributedCache;
using AppStore.Common.Infrastructure.DistributedCache.Base;
using FluentValidation;
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
                .AddValidator()
                .AddDistributedCache(configuration)
                .AddCachingServices();

        #endregion

        #region Private Methods

        private static IServiceCollection AddMediator(this IServiceCollection services)
            => services.AddMediatR(typeof(ServiceCollectionExtension).Assembly);

        private static IServiceCollection AddValidator(this IServiceCollection services)
            => services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtension).Assembly)
                       .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

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
