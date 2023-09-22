using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Customers.Queries.Get
{
    public class GetProjectMapper : Profile
    {
        public GetProjectMapper()
        {
            CreateMap<Customer, GetCustomersResponse>();
            CreateMap<PagedResults<Customer>, PagedResults<GetCustomersResponse>>();
        }
    }
}