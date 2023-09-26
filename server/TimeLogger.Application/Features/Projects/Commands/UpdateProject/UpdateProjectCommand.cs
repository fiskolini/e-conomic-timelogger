using MediatR;

namespace TimeLogger.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommand : IRequest<UpdateProjectResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Deadline { get; set; } = ""; // classical Nullable value types problem
        public int? TimeAllocated { get; set; }
        public string CompletedAt { get; set; }
    }
}