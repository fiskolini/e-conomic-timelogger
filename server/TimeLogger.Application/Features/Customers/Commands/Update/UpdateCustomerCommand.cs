using MediatR;

namespace TimeLogger.Application.Features.Customers.Commands.Update
{
    public class UpdateCustomerCommand : IRequest<UpdateCustomerResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}