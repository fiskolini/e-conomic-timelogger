using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Times.Queries.Get
{
    public class GetTimesMapper : Profile
    {
        public GetTimesMapper()
        {
            CreateMap<Time, GetTimesResponse>();
            CreateMap<PagedResults<Time>, PagedResults<GetTimesResponse>>();
        }
    }
}