using System;

namespace AppStore.Common.Domain.Entities
{
    public abstract class Entity<TId>
    {
        public TId Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
