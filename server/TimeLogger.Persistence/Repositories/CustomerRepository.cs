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
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext context) : base(context)
        {
        }

        public Task<int> GetProjectsCount(int customerId, CancellationToken cancellationToken,
            bool considerDeleted = false)
        {
            var query = GetQuery(considerDeleted)
                .Where(x => x.Id == customerId)
                .Select(x => x.Projects.Count);

            return query.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Dictionary<int, int>> GetProjectsCounts(List<int> customerIds,
            CancellationToken cancellationToken, bool considerDeleted = false)
        {
            var query = GetQuery(considerDeleted)
                .Where(x => customerIds.Contains(x.Id))
                .Select(x => new { count = x.Projects.Count, id = x.Id })
                .ToDictionary(k => k.id, i => i.count);

            return query;
        }

        public async Task<PagedResults<Customer>> GetAll(PagedRequest request, CancellationToken cancellationToken,
            bool considerDeleted = false)
        {
            var query = GetQuery(considerDeleted);
            if (request.Search != null)
            {
                query = query.Where(x => x.Name.Contains(request.Search) || x.Id.ToString() == request.Search);
            }

            var totalItems = await query.CountAsync(cancellationToken);
            var items = await query
                .WithPagedResults(request)
                .ToListAsync(cancellationToken);

            return new PagedResults<Customer>
            {
                Data = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalItems = totalItems
            };
        }
    }
}