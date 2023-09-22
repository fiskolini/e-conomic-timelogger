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
                .Where(x => x.CustomerId == customerId);

            var totalItems = await query.CountAsync(cancellationToken);

            query.WithPagedResults(request);

            var results = await GetAll(query, cancellationToken);

            return new PagedResults<Project>
            {
                Data = results,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalItems = totalItems
            };
        }
    }
}