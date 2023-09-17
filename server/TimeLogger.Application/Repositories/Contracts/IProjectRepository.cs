using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Repositories.Contracts
{
    public interface IProjectRepository : IBaseRepository<Project>
    {
        void RegisterTime(uint id);
        Task<List<Project>> GetTimeRegistration(uint id, CancellationToken cancellationToken);
    }
}