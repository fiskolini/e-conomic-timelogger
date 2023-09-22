using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Projects.Queries.Get
{
    public class GetProjectMapper : Profile
    {
        public GetProjectMapper()
        {
            CreateMap<Project, GetProjectResponse>();
            CreateMap<PagedResults<Project>, PagedResults<GetProjectResponse>>();
        }
    }
}