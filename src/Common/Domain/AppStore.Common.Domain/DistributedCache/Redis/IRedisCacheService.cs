using System.Threading;
using System.Threading.Tasks;

namespace AppStore.Common.Domain.DistributedCache.Redis
{
    /// <summary>
    /// Redis communication interface 
    /// </summary>
    public interface IRedisCacheService
    {
        /// <summary>
        /// Persist an object in redis
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <param name="cacheKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetAsync<TEntity>(TEntity entity, string cacheKey, CancellationToken cancellationToken = default);

        /// <summary>
        /// Returns a redis record according to the given key
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="cacheKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync<TEntity>(string cacheKey, CancellationToken cancellationToken = default);
    }
}
