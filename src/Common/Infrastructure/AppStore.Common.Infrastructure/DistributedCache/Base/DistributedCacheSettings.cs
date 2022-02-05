using AppStore.Common.Domain.DistributedCache.Base;
using System;

namespace AppStore.Common.Infrastructure.DistributedCache.Base
{
    public sealed class DistributedCacheSettings : IDistributedCacheSettings
    {
        public bool Enabled { get; set; }
        public string ConnectionString { get; set; }
        public TimeSpan ExpirationTime { get; set; }
    }
}
