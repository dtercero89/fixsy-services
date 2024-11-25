
using System.Data.Common;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using FixsyWebApi.Data.Agg;
using FixsyWebApi.Data.UnitOfWork;

namespace FixsyWebApi.Data.Repository
{
    public interface IGenericRepository<T> : IDisposable
        where T : IQueryableUnitOfWork
    {

        /// <summary>
        /// Get the unit of work in this repository
        /// </summary>
        IUnitOfWork UnitOfWork { get; }

        /// <summary>
        /// Add the entity to the repository.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The new entity to add.</param>
        void Add<TEntity>(TEntity entity)
            where TEntity : EntityBase;

        /// <summary>
        /// Add the entities to the repository.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entities">The new entities to add.</param>
        void AddRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : EntityBase;

        /// <summary>
        /// Remove the specified entity.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entity">The entity to remove.</param>
        void Remove<TEntity>(TEntity entity)
            where TEntity : EntityBase;

        /// <summary>
        /// Remove the specified entities from the repository.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="entities">The entities to remove.</param>
        void RemoveRange<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : EntityBase;

        /// <summary>
        /// Get all rows.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>Task{List{`0}}.</returns>
        IEnumerable<TEntity> GetAll<TEntity>()
            where TEntity : EntityBase;

        /// <summary>
        /// Get all rows asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <returns>Task{List{`0}}.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>()
            where TEntity : EntityBase;

        /// <summary>
        /// Get all rows.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="includes">Related entities to include in the result set.</param>
        /// <returns>Task{List{`0}}.</returns>
        IEnumerable<TEntity> GetAll<TEntity>(List<string> includes)
            where TEntity : EntityBase;

        /// <summary>
        /// Get all rows asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="includes">Related entities to include in the result set.</param>
        /// <returns>Task{List{`0}}.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(List<string> includes)
            where TEntity : EntityBase;



        /// <summary>
        /// Get the First or default row filtered by the Query expression.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="predicate">The where (the query expression).</param>
        /// <returns>Object of the TEntity class.</returns>
        TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : EntityBase;

        /// <summary>
        /// Get the First or default row filtered by the Query expression asynchronously.
        /// </summary>
        /// <typeparam name="TEntity">The entity type.</typeparam>
        /// <param name="predicate">The where (the query expression).</param>
        /// <returns>Object of the TEntity class.</returns>
        Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : EntityBase;
        //void ExecuteSqlJob(string nameOfConnectionString, string sqlJobName);

        /// <summary>
        /// Get an element of type TEntity in repository
        /// </summary>
        /// <param name="predicate">Filter that the element do match</param>
        /// <param name="includes">Related entities to include in the result set.</param>
        /// <returns>selected element</returns>
        TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes)
            where TEntity : EntityBase;

        /// <summary>
        /// Get an element of type TEntity in repository
        /// </summary>
        /// <param name="predicate">Filter that the element do match</param>
        /// <param name="includes">Related entities to include in the result set.</param>
        /// <returns>selected element</returns>
        Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes)
            where TEntity : EntityBase;

        /// <summary>
        /// Gets filtered entities.
        /// </summary>
        /// <param name="predicate">The where.</param>
        /// <returns>Enumerable of the TEntity class.</returns>
        IEnumerable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : EntityBase;

        /// <summary>
        /// Gets the many asynchronous.
        /// </summary>
        /// <param name="predicate">The where.</param>
        /// <returns>Enumerable of the TEntity class.</returns>
        Task<IEnumerable<TEntity>> GetFilteredAsync<TEntity>(Expression<Func<TEntity, bool>> predicate)
            where TEntity : EntityBase;

        /// <summary>
        /// Gets filtered entities.
        /// </summary>
        /// <param name="predicate">The where.</param>
        /// <returns>Enumerable of the TEntity class.</returns>
        IEnumerable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes)
            where TEntity : EntityBase;

        /// <summary>
        /// Gets the many asynchronous.
        /// </summary>
        /// <param name="predicate">The where.</param>
        /// <returns>Enumerable of the TEntity class.</returns>
        Task<IEnumerable<TEntity>> GetFilteredAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes)
            where TEntity : EntityBase;

        Task<List<TEntity>> GetPagedAndFilteredAsync<TEntity>(int pageIndex, int pageCount, Dictionary<string, object> filters)
              where TEntity : EntityBase;
  


          IEnumerable<TEntity> GetPagedAndFiltered<TEntity>(int pageIndex, int pageCount, Dictionary<string, object> filters)
             where TEntity : EntityBase;

        IEnumerable<TEntity> GetPagedAndFiltered<TEntity>(int pageIndex, int pageCount, Dictionary<string, object> filters, string orderByField, bool ascending)
             where TEntity : EntityBase;

        void Modify<TEntity>(TEntity item) where TEntity : EntityBase;

        //IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, List<DbParameter> parameters) where TEntity : new();

        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, Dictionary<string, object> parameters);
        IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, IEnumerable<object> parameters);


    }
}
