using MediatR;

namespace TimeLogger.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<CreateProjectResponse>
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Deadline { get; set; }
        public int TimeAllocated { get; set; }
    }
}