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
    }
}