using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Customers.Queries.Get
{
    public class GetCustomersMapper : Profile
    {
        public GetCustomersMapper()
        {
            CreateMap<Customer, GetCustomersResponse>();
            CreateMap<PagedResults<Customer>, PagedResults<GetCustomersResponse>>();
        }
    }
}