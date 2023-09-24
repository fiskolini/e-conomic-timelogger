using AutoMapper;
using TimeLogger.Application.Features.Projects.Commands.CreateProject;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectMapper : Profile
    {
        public DeleteProjectMapper()
        {
            CreateMap<DeleteProjectCommand, Project>();
            CreateMap<Project, DeleteProjectResponse>();
        }
    }
}