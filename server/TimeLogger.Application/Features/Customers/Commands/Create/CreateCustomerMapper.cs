using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Customers.Commands.Create
{
    public class CreateCustomerMapper : Profile
    {
        public CreateCustomerMapper()
        {
            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<Customer, CreateCustomerResponse>();
        }
    }
}