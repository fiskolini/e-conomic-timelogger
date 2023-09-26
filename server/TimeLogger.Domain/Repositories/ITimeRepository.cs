using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Domain.Repositories
{
    public interface ITimeRepository : IBaseRepository<Time>
    {
        /// <summary>
        /// Get all projects related to the given Project Id
        /// </summary>
        /// <param name="projectId">Needle to filter</param>
        /// <param name="request">Paged request data</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="considerDeleted">Consider entities soft deleted</param>
        Task<PagedResults<Time>> GetAllByProjectId(int projectId, PagedRequest request,
            CancellationToken cancellationToken,
            bool considerDeleted = false
        );

        public Task<Dictionary<int, int>> GetTotalMinutesPerProject(List<int> projectIds,
            CancellationToken cancellationToken, bool considerDeleted = false);
    }
}