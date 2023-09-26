using System;
using System.Threading;
using System.Threading.Tasks;

namespace TimeLogger.Domain.Repositories.Common
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Begins a database transaction asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the transaction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Commits the current database transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// Rolls back the current database transaction.
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Commits pending changes to the database asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation and returning the number of affected records.</returns>
        Task<int> Commit(CancellationToken cancellationToken);
    }
}