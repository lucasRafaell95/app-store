using AppStore.Common.Domain.Entities;
using AppStore.Common.Domain.Persistence.Base;
using AppStore.Common.Domain.Persistence.MongoDB.Base;
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

        protected readonly IMongoClient client;
        protected readonly IMongoCollection<TEntity> collection;

        #endregion

        #region Constructor 

        protected Repository(IMongoSettings settings)
        {
            settings = settings ?? throw new ArgumentNullException(nameof(settings));

            this.client = new MongoClient(settings.ConnectionString);

            var database = this.client.GetDatabase(settings.DatabaseName);

            this.collection = database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        #endregion

        #region IRepository Methods

        public virtual async Task Create(TEntity entity)
            => await collection.InsertOneAsync(entity);

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

        public virtual async Task Update(TEntity entity)
            => await collection.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", entity.Id), entity);

        public virtual async Task Delete(Guid id)
            => await collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));

        void IDisposable.Dispose() { }

        #endregion
    }
}
