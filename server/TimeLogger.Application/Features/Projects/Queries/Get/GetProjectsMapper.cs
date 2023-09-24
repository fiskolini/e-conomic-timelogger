using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Projects.Queries.Get
{
    public class GetProjectsMapper : Profile
    {
        public GetProjectsMapper()
        {
            CreateMap<Project, GetProjectsResponse>();
            CreateMap<PagedResults<Project>, PagedResults<GetProjectsResponse>>();
        }
    }
}