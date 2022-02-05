using System;

namespace AppStore.Common.Domain.DistributedCache.Base
{
    public interface IDistributedCacheSettings
    {
        bool Enabled { get; set; }
        string ConnectionString { get; set; }
        TimeSpan ExpirationTime { get; set; }
    }
}
