using AppStore.Common.Domain.Entities;
using AppStore.Common.Domain.Persistence.Base;
using AppStore.Common.Domain.Persistence.MongoDB;
using MongoDB.Driver;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStore.Common.Infrastructure.Persistance.Base
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        #region Fields

        protected readonly IMongoContext context;
        protected IMongoCollection<TEntity> collection;

        #endregion

        #region Constructor 

        protected Repository(IMongoContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));

            collection = context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        #endregion

        #region IRepository Methods

        public virtual void Create(TEntity entity)
            => context.AddCommand(() => collection.InsertOneAsync(entity));

        public virtual async Task<TEntity> GetById(Guid id)
        {
            var data = await collection.FindAsync(Builders<TEntity>.Filter.Eq("_id", id));

            return data.FirstOrDefault();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            var all = await collection.FindAsync(Builders<TEntity>.Filter.Empty);

            return all.ToList();
        }

        public virtual void Update(TEntity entity)
        {
            context.AddCommand(() =>
                collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.Id), entity));
        }

        public virtual void Delete(Guid id)
            => context.AddCommand(() => collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id)));

        public void Dispose()
            => context?.Dispose();

        #endregion
    }
}
