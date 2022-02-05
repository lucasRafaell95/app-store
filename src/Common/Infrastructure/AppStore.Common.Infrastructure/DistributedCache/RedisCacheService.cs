using AppStore.Common.Domain.DistributedCache.Base;
using AppStore.Common.Domain.DistributedCache.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppStore.Common.Infrastructure.DistributedCache
{
    public sealed class RedisCacheService<TDistributedCacheSettings> : IRedisCacheService where TDistributedCacheSettings : IDistributedCacheSettings
    {
        #region Fields

        private readonly ILogger logger;
        private readonly IDistributedCache cache;
        private readonly IDistributedCacheSettings settings;

        #endregion

        #region Constructor

        public RedisCacheService(ILogger<RedisCacheService<TDistributedCacheSettings>> logger,
                                 IDistributedCache cache,
                                 IDistributedCacheSettings settings)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        #endregion

        #region IRedisCacheService Methods

        public async Task SetAsync<TEntity>(TEntity entity, string cacheKey, CancellationToken cancellationToken = default)
        {
            try
            {
                if (settings.Enabled)
                {
                    var json = JsonConvert.SerializeObject(entity);

                    var content = Encoding.UTF8.GetBytes(json);

                    await cache.SetAsync(cacheKey, content, new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = settings.ExpirationTime
                    }, cancellationToken);

                    this.logger.LogInformation($"Inserting the value into the cache with the key {cacheKey}", new
                    {
                        CacheKey = cacheKey
                    });
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("Error trying to set value in redis", new
                {
                    Key = cacheKey,
                    Exception = ex
                });
            }
        }

        public async Task<TEntity> GetAsync<TEntity>(string cacheKey, CancellationToken cancellationToken = default)
        {
            TEntity result = default;

            try
            {
                if (settings.Enabled)
                {
                    var value = await cache.GetAsync(cacheKey, cancellationToken);

                    result = (value is null)
                        ? result
                        : DeserializeContent(value);
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError("An error occurred while trying to retrieve the cache information", new
                {
                    CacheKey = cacheKey,
                    Exception = ex
                });
            }

            return result;

            static TEntity DeserializeContent(byte[] cachedValue)
                => JsonConvert.DeserializeObject<TEntity>(Encoding.UTF8.GetString(cachedValue));
        }

        #endregion
    }
}
