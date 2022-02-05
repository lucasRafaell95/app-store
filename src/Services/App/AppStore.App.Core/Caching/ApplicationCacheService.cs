using AppStore.App.Core.Caching.Contracts;
using AppStore.App.Core.Models.Cacheable;
using AppStore.Common.Domain.DistributedCache.Base;
using AppStore.Common.Domain.DistributedCache.Redis;
using AppStore.Common.Domain.Entities;
using Mapster;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace AppStore.App.Core.Caching
{
    public sealed class ApplicationCacheService : IApplicationCacheService
    {
        #region Fields

        private readonly ILogger logger;
        private readonly IRedisCacheService cacheService;
        private readonly IDistributedCacheSettings settings;

        #endregion

        #region Constructor

        public ApplicationCacheService(ILogger<ApplicationCacheService> logger,
                                       IRedisCacheService cacheService,
                                       IDistributedCacheSettings settings)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        #endregion

        #region IApplicationCacheService Methods

        public async Task<List<Application>> GetApplicationsAsync(Func<Task<List<Application>>> dataSource, [CallerMemberName] string methodName = null)
        {
            if (!settings.Enabled)
            {
                this.logger.LogInformation("Caching is disabled returning data from datasource");

                return await dataSource?.Invoke();
            }

            var cacheKey = "APPSTORE:APPLICATIONS";

            try
            {
                var cachedData = await cacheService.GetAsync<List<ApplicationCacheable>>(cacheKey);

                if (cachedData != null && cachedData.Any())
                {
                    this.logger.LogInformation($"Returning cached data for \"{cacheKey}\" from distributed cache.");

                    return cachedData.Adapt<List<Application>>();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, methodName, new
                {
                    ErrorGathering = "DistributedCache",
                    WithCacheKey = cacheKey
                });
            }

            var result = await dataSource?.Invoke();

            if (result != null && result.Any())
            {
                this.logger.LogInformation("Returning data from data source and storing in distributed cache.");

                await cacheService.SetAsync(result.Adapt<List<ApplicationCacheable>>(), cacheKey);
            }

            return result;
        }

        #endregion
    }
}
