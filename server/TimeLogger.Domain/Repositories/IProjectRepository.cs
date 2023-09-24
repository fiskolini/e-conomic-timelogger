using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Domain.Repositories
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        /// <summary>
        /// Get single project from customer ID needle
        /// </summary>
        /// <param name="customerId">Needle to filter</param>
        /// <param name="projectId">Project ID</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="considerDeleted">Consider entities soft deleted</param>
        public Task<Project> GetSingle(
            int customerId, int projectId,
            CancellationToken cancellationToken,
            bool considerDeleted = false
        );

        /// <summary>
        /// Get all projects related to the given Customer Id
        /// </summary>
        /// <param name="customerId">Needle to filter</param>
        /// <param name="request">Paged request data</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="considerDeleted">Consider entities soft deleted</param>
        Task<PagedResults<Project>> GetAllByCustomerId(
            int customerId, PagedRequest request,
            CancellationToken cancellationToken,
            bool considerDeleted = false
        );

        /// <summary>
        /// Get project counts for given customer ids
        /// </summary>
        /// <param name="customerIds">Needle to filter</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="considerDeleted">Consider entities soft deleted</param>
        public Task<Dictionary<int, int>> GetCountForCustomers(List<int> customerIds,
            CancellationToken cancellationToken, bool considerDeleted = false);
    }
}