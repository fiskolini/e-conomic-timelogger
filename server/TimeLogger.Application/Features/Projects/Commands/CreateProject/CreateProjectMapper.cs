using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectMapper : Profile
    {
        public CreateProjectMapper()
        {
            CreateMap<CreateProjectCommand, Project>();
            CreateMap<Project, CreateProjectResponse>();
        }
    }
}