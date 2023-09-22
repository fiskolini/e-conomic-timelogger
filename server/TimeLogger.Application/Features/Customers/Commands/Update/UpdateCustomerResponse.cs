using MediatR;
using TimeLogger.Application.Common.Responses;

namespace TimeLogger.Application.Features.Customers.Commands.Update
{
    public class UpdateCustomerResponse : BaseEntityResponse, IRequest<UpdateCustomerCommand>
    {
        public string Name { get; set; }
    }
}