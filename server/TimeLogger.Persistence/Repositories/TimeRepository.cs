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
    public class TimeRepository : BaseRepository<Time>, ITimeRepository
    {
        public TimeRepository(DataContext context) : base(context)
        {
        }
        
        public Task<Dictionary<int, int>> GetTotalMinutesPerProject(List<int> projectIds,
            CancellationToken cancellationToken, bool considerDeleted = false)
        {
            var query = GetQuery(considerDeleted)
                .Select(project => new
                {
                    ProjectId = project.Id,
                    TimeSpent = Context.Times
                        .WhereCanBeSoftDeleted(considerDeleted)
                        .Where(time => time.ProjectId == project.Id)
                        .Sum(time => time.Minutes)
                })
                .Where(time => projectIds.Contains(time.ProjectId))
                .ToDictionary(k => k.ProjectId, i => i.TimeSpent);

            return Task.FromResult(query);
        }

        public async Task<PagedResults<Time>> GetAllByProjectId(int projectId, PagedRequest request,
            CancellationToken cancellationToken,
            bool considerDeleted = false)
        {
            // Build the initial query to retrieve time entries by project ID
            var query = GetQuery(considerDeleted)
                .Where(x => x.ProjectId == projectId);

            // Calculate the total number of time entries that match the query
            var totalItems = await query.CountAsync(cancellationToken);

            // Retrieve a page of time entries based on the provided paging parameters
            var items = await query
                .WithPagedResults(request) // Apply paging
                .ApplySort(request.OrderBy) // Apply sorting
                .ToListAsync(cancellationToken);

            // Create and return a PagedResults object containing the retrieved time entries
            return new PagedResults<Time>
            {
                Data = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalItems = totalItems
            };
        }
    }
}