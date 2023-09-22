using System;
using System.Threading;
using System.Threading.Tasks;

namespace TimeLogger.Domain.Repositories.Common
{
    public interface IUnitOfWork : IDisposable
    {
        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        void CommitTransaction();
        void RollbackTransaction();

        Task<int> Commit(CancellationToken cancellationToken);
    }
}