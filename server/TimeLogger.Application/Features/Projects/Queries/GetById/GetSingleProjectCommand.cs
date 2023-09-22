using MediatR;
using TimeLogger.Application.Common.Commands;

namespace TimeLogger.Application.Features.Projects.Queries.GetById
{
    public class GetSingleProjectCommand : BaseEntityCommand, IRequest<ProjectResponse>
    {
        public bool ConsiderDeleted { get; set; } = false;
    }
}