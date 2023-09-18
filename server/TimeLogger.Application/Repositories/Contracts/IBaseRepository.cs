using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeLogger.Domain.Common;

namespace TimeLogger.Application.Repositories.Contracts
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> Get(uint id, CancellationToken cancellationToken);
        Task<List<T>> GetAll(CancellationToken cancellationToken);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}