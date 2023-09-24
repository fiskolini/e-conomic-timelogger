using MediatR;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Projects.Queries.Get
{
    public sealed class GetProjectsCommand : PagedRequest, IRequest<PagedResults<GetProjectsResponse>>
    {
        public int CustomerId { get; set; }
        public bool ConsiderDeleted { get; set; } = false;
    }
}