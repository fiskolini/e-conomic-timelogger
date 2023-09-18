using System.Threading;
using System.Threading.Tasks;

namespace TimeLogger.Application.Repositories.Contracts
{
    public interface IUnitOfWork
    {
        Task Save(CancellationToken cancellationToken);
    }
}