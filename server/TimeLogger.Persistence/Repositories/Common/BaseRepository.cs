using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeLogger.Domain.Common;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories.Common;
using TimeLogger.Persistence.Common.Extensions;
using TimeLogger.Persistence.Context;

namespace TimeLogger.Persistence.Repositories.Common
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DataContext Context;

        public BaseRepository(DataContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Create given entity
        /// </summary>
        /// <param name="entity">Entity to create</param>
        public void Create(T entity)
        {
            entity.DateCreated = DateTimeOffset.Now;

            Context.Add(entity);
        }

        /// <summary>
        /// Create given range of entities
        /// </summary>
        /// <param name="entities">List of entities to create</param>
        public void CreateRange(List<T> entities)
        {
            foreach (var singleEntity in entities)
            {
                singleEntity.DateCreated = DateTimeOffset.Now;
            }

            Context.AddRange(entities);
        }

        /// <summary>
        /// Get all items based on given query
        /// </summary>
        /// <param name="query">Query to plug</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected Task<List<T>> GetAll(IQueryable<T> query, CancellationToken cancellationToken)
        {
            return query.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Get entity by the given Id
        /// </summary>
        /// <param name="id">Id to filter</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="considerDeleted">Consider entities soft deleted</param>
        public Task<T> GetSingle(int id, CancellationToken cancellationToken, bool considerDeleted = false)
        {
            var query = GetQuery(considerDeleted);

            return query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        /// <summary>
        /// Get initial query considering Soft Delete
        /// </summary>
        /// <param name="considerDeleted"></param>
        /// <returns></returns>
        protected IQueryable<T> GetQuery(bool considerDeleted = false)
        {
            return Context.Set<T>().AsQueryable()
                .WhereCanBeSoftDeleted(considerDeleted);
        }

        /// <summary>
        /// Get paged entities
        /// </summary>
        /// <param name="request">Pagination request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="considerDeleted">Consider entities soft deleted</param>
        public async Task<PagedResults<T>> GetAll(PagedRequest request, CancellationToken cancellationToken,
            bool considerDeleted = false)
        {
            var query = GetQuery(considerDeleted);

            var totalItems = await query.CountAsync(cancellationToken);
            var items = await query
                .WithPagedResults(request)
                .ToListAsync(cancellationToken);

            return new PagedResults<T>
            {
                Data = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalItems = totalItems
            };
        }

        /// <summary>
        /// Update given entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        public void Update(T entity)
        {
            entity.DateUpdated = DateTimeOffset.Now;

            Context.Update(entity);
        }

        /// <summary>
        /// Soft delete given entity
        /// </summary>
        /// <param name="entity">Entity to Soft Delete</param>
        public void SoftDelete(T entity)
        {
            entity.DateDeleted = DateTimeOffset.Now;

            Context.Update(entity);
        }

        /// <summary>
        /// Count all results
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <param name="considerDeleted">Consider entities soft deleted</param>
        public Task<int> Count(bool considerDeleted, CancellationToken cancellationToken)
        {
            var query = GetQuery(considerDeleted);
            return query.CountAsync(cancellationToken);
        }
    }
}