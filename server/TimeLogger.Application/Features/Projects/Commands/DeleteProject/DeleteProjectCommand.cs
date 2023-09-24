using MediatR;
using TimeLogger.Application.Features.Projects.Commands.CreateProject;

namespace TimeLogger.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest<DeleteProjectResponse>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Deadline { get; set; }
    }
}