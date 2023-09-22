using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeLogger.Domain.Common;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Domain.Repositories.Common
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Create given entity
        /// </summary>
        /// <param name="entity">Entity to create</param>
        void Create(T entity);

        /// <summary>
        /// Create given range of entities
        /// </summary>
        /// <param name="entities">List of entities to create</param>
        void CreateRange(List<T> entities);

        /// <summary>
        /// Get entity by the given Id
        /// </summary>
        /// <param name="id">Id to filter</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="considerDeleted">Consider entities soft deleted</param>
        Task<T> GetSingle(int id, CancellationToken cancellationToken, bool considerDeleted = false);

        /// <summary>
        /// Get paged entities
        /// </summary>
        /// <param name="request">Pagination request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="considerDeleted">Consider entities soft deleted</param>
        Task<PagedResults<T>> GetAll(PagedRequest request, CancellationToken cancellationToken,
            bool considerDeleted = false
        );

        /// <summary>
        /// Update given entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        void Update(T entity);

        /// <summary>
        /// Soft delete given entity
        /// </summary>
        /// <param name="entity">Entity to Soft Delete</param>
        void SoftDelete(T entity);
    }
}