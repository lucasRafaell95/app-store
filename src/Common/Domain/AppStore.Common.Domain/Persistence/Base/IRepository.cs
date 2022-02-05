using AppStore.Common.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AppStore.Common.Domain.Persistence.Base
{
    /// <summary>
    /// Base interface for database communication
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        /// <summary>
        /// Creates a new record in the database
        /// </summary>
        /// <param name="entity"></param>
        void Create(TEntity entity);

        /// <summary>
        /// Returns record according to the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetById(Guid id);

        /// <summary>
        /// Returns all records of a given type
        /// </summary>
        /// <returns></returns>
        Task<List<TEntity>> GetAll();

        /// <summary>
        /// Updates the information of a record in the database
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Delete a record according to the given Id
        /// </summary>
        /// <param name="id"></param>
        void Delete(Guid id);
    }
}
