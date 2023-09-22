namespace TimeLogger.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectCommand
    {
        public string Name { get; set; }
        public string Deadline { get; set; }
        public int TimeAllocated { get; set; }
    }
}