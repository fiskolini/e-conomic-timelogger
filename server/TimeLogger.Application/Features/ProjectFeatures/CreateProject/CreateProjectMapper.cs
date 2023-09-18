using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.ProjectFeatures.CreateProject
{
    public sealed class CreateProjectMapper : Profile
    {
        public CreateProjectMapper()
        {
            CreateMap<CreateProjectRequest, Project>();
            CreateMap<Project, CreateProjectResponse>();
        }
    }
}