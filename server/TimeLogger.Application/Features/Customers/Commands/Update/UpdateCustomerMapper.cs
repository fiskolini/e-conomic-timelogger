using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Customers.Commands.Update
{
    public class UpdateCustomerMapper : Profile
    {
        public UpdateCustomerMapper()
        {
            CreateMap<UpdateCustomerCommand, Customer>();
            CreateMap<Customer, UpdateCustomerResponse>();
        }
    }
}