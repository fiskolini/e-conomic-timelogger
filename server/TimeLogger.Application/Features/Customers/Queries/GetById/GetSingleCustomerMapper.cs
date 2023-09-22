using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Customers.Queries.GetById
{
    public class GetSingleCustomerMapper : Profile
    {
        public GetSingleCustomerMapper()
        {
            CreateMap<GetSingleCustomerCommand, Customer>();
            CreateMap<Customer, GetSingleCustomerResponse>();
        }
    }
}