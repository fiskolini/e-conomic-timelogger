using MediatR;
using TimeLogger.Application.Common.Commands;

namespace TimeLogger.Application.Features.Projects.Queries.GetById
{
    public class GetSingleProjectCommand : BaseEntityCommand, IRequest<GetSingleProjectResponse>
    {
        public int CustomerId { get; set; }
        public bool ConsiderDeleted { get; set; } = false;
    }
}