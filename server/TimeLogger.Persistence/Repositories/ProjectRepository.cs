using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Persistence.Common.Extensions;
using TimeLogger.Persistence.Context;
using TimeLogger.Persistence.Repositories.Common;

namespace TimeLogger.Persistence.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DataContext context) : base(context)
        {
        }

        /// <summary>
        /// Get single project from customer ID needle
        /// </summary>
        /// <param name="customerId">Needle to filter</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="considerDeleted">Consider entities soft deleted</param>
        public Task<Project> GetSingle(int customerId, int projectId, CancellationToken cancellationToken,
            bool considerDeleted = false)
        {
            var query = GetQuery(considerDeleted)
                .Where(x => x.CustomerId == customerId && x.Id == projectId);

            return query.FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// Get all projects related to the given Customer Id
        /// </summary>
        /// <param name="customerId">Needle to filter</param>
        /// <param name="request">Paged request data</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="considerDeleted">Consider entities soft deleted</param>
        public async Task<PagedResults<Project>> GetAllByCustomerId(int customerId, PagedRequest request,
            CancellationToken cancellationToken, bool considerDeleted = false)
        {
            var query = Context.Set<Project>().AsQueryable()
                .WhereCanBeSoftDeleted(considerDeleted)
                .WhereNameContains(request.Search)
                .Where(x => x.CustomerId == customerId);

            var totalItems = await query.CountAsync(cancellationToken);

            query = query.WithPagedResults(request);

            var results = await GetAll(query, cancellationToken);

            return new PagedResults<Project>
            {
                Data = results,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalItems = totalItems
            };
        }


        /// <summary>
        /// Get project counts for given customer ids
        /// </summary>
        /// <param name="customerIds">Needle to filter</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="considerDeleted">Consider entities soft deleted</param>
        public Task<Dictionary<int, int>> GetCountForCustomers(List<int> customerIds,
            CancellationToken cancellationToken, bool considerDeleted = false)
        {
            var query = GetQuery(considerDeleted)
                .Select(customer => new
                {
                    CustomerId = customer.Id,
                    ProjectCount = Context.Projects
                        .WhereCanBeSoftDeleted(considerDeleted)
                        .Count(project => project.CustomerId == customer.Id)
                })
                .Where(project => customerIds.Contains(project.CustomerId))
                .ToDictionary(k => k.CustomerId, i => i.ProjectCount);

            return Task.FromResult(query);
        }
    }
}