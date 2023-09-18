using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TimeLogger.Application.Repositories.Contracts;
using TimeLogger.Domain.Entities;
using TimeLogger.Persistence.Context;

namespace TimeLogger.Persistence.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DataContext context) : base(context)
        {
        }
        
        public void RegisterTime(uint id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Project>> GetTimeRegistration(uint id, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}