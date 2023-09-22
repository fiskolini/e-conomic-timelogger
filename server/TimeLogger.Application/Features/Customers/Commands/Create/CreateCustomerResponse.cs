using MediatR;
using TimeLogger.Application.Common.Responses;

namespace TimeLogger.Application.Features.Customers.Commands.Create
{
    public class CreateCustomerResponse : BaseEntityResponse, IRequest<CreateCustomerCommand>
    {
        public string Name { get; set; }
    }
}