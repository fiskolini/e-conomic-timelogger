using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectMapper : Profile
    {
        public DeleteProjectMapper()
        {
            CreateMap<DeleteProjectCommand, DeleteProjectCommand>();
            CreateMap<Project, ProjectResponse>();
        }
    }
}