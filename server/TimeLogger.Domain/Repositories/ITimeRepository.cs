using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Domain.Repositories
{
    public interface ITimeRepository : IBaseRepository<Time>
    {
    }
}