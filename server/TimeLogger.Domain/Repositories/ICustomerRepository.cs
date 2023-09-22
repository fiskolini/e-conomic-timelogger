using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Domain.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// Get projects count for given customer
        /// </summary>
        /// <param name="customerId">Customer needle</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="considerDeleted">Consider entities soft deleted</param>
        public Task<int> GetProjectsCount(int customerId, CancellationToken cancellationToken,
            bool considerDeleted = false);

        /// <summary>
        /// Get project counts for given customer ids
        /// </summary>
        /// <param name="customerIds">Customers needle</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <param name="considerDeleted">Consider entities soft deleted</param>
        /// <returns></returns>
        public Task<Dictionary<int, int>> GetProjectsCounts(List<int> customerIds, CancellationToken cancellationToken,
            bool considerDeleted = false);
    }
}