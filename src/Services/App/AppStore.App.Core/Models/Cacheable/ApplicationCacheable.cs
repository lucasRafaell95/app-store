using System;

namespace AppStore.App.Core.Models.Cacheable
{
    public sealed record ApplicationCacheable
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public double Price { get; init; }
        public string Description { get; init; }
    }
}
