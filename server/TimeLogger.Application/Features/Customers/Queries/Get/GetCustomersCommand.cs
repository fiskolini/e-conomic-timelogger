using MediatR;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Customers.Queries.Get
{
    public sealed class GetCustomersCommand : PagedRequest, IRequest<PagedResults<GetCustomersResponse>>
    {
        public bool ConsiderDeleted { get; set; } = false;
    }
}