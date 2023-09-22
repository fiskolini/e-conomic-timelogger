using MediatR;
using TimeLogger.Application.Common.Commands;

namespace TimeLogger.Application.Features.Customers.Queries.GetById
{
    public class GetSingleCustomerCommand : BaseEntityCommand, IRequest<GetSingleCustomerResponse>
    {
        public bool ConsiderDeleted { get; set; } = false;
    }
}