using AutoMapper;
using TimeLogger.Application.Features.Customers.Queries.GetById;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Times.Queries.GetById
{
    public class GetSingleTimeMapper : Profile
    {
        public GetSingleTimeMapper()
        {
            CreateMap<GetSingleCustomerCommand, Customer>();
            CreateMap<Customer, GetSingleCustomerResponse>();
        }
    }
}