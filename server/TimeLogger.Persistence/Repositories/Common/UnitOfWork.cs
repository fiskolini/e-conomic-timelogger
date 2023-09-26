using System.Threading;
using System.Threading.Tasks;
using TimeLogger.Domain.Repositories.Common;
using TimeLogger.Persistence.Context;

namespace TimeLogger.Persistence.Repositories.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Begins a database transaction asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the transaction.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <summary>
        /// Commits the current database transaction.
        /// </summary>
        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        /// <summary>
        /// Rolls back the current database transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        /// <summary>
        /// Commits pending changes to the database asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation and returning the number of affected records.</returns>
        public Task<int> Commit(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Disposes of the database context and resources.
        /// </summary>
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}