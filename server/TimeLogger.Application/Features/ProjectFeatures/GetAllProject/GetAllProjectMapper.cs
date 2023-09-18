using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.ProjectFeatures.GetAllProject
{
    public class GetAllProjectMapper : Profile
    {
        public GetAllProjectMapper()
        {
            CreateMap<Project, GetAllProjectResponse>();
        }
    }
}