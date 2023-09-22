using MediatR;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Projects.Queries.Get
{
    public sealed class GetProjectCommand : PagedRequest, IRequest<PagedResults<GetProjectResponse>>
    {
        public int CustomerId { get; set; }
        public bool ConsiderDeleted { get; set; } = false;
    }
}