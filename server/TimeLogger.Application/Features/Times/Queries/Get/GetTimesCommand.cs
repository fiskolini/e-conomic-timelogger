using MediatR;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Times.Queries.Get
{
    public sealed class GetTimesCommand : PagedRequest, IRequest<PagedResults<GetTimesResponse>>
    {
        public bool ConsiderDeleted { get; set; } = false;
    }
}