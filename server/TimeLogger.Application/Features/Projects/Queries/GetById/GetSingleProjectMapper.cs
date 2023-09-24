using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Projects.Queries.GetById
{
    public class GetSingleProjectMapper : Profile
    {
        public GetSingleProjectMapper()
        {
            CreateMap<GetSingleProjectCommand, Project>();
            CreateMap<Project, GetSingleProjectResponse>();
        }
    }
}