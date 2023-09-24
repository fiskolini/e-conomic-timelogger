using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Persistence.Context;
using TimeLogger.Persistence.Repositories.Common;

namespace TimeLogger.Persistence.Repositories
{
    public class TimeRepository: BaseRepository<Time>, ITimeRepository
    {
        public TimeRepository(DataContext context) : base(context)
        {
        }
    }
}