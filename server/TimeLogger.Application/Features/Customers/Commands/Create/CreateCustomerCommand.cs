using MediatR;

namespace TimeLogger.Application.Features.Customers.Commands.Create
{
    public class CreateCustomerCommand : IRequest<CreateCustomerResponse>
    {
        public string Name { get; set; }
    }
}