using MediatR;

namespace TimeLogger.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest<UpdateProjectResponse>
    {
        public int CustomerId { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Deadline { get; set; }
        public int? TimeAllocated { get; set; }
        public string CompletedAt { get; set; }
    }
}