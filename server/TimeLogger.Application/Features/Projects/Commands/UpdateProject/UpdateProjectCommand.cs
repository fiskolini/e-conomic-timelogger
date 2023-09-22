namespace TimeLogger.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectCommand : SingleProjectRequest
    {
        public string Name { get; set; }
        public string Deadline { get; set; }
        public int? TimeAllocated { get; set; }
        public string CompletedAt { get; set; }
    }
}