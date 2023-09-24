using AutoMapper;
using TimeLogger.Application.Features.Projects.Commands.CreateProject;
using TimeLogger.Application.Features.Projects.Commands.DeleteProject;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectMapper : Profile
    {
        public UpdateProjectMapper()
        {
            CreateMap<UpdateProjectCommand, Project>();
            CreateMap<Project, UpdateProjectResponse>();
        }
    }
}