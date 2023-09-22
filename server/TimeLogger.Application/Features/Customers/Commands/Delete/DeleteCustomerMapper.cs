using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Customers.Commands.Delete
{
    public class DeleteCustomerMapper : Profile
    {
        public DeleteCustomerMapper()
        {
            CreateMap<DeleteCustomerCommand, Customer>();
            CreateMap<Customer, DeleteCustomerResponse>();
        }
    }
}