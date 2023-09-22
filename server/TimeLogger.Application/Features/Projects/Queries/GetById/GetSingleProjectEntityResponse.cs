using TimeLogger.Application.Common.Responses;

namespace TimeLogger.Application.Features.Projects.Queries.GetById
{
    public class GetSingleProjectEntityResponse : BaseEntityResponse
    {
        public bool ConsiderDeleted { get; set; } = false;
    }
}