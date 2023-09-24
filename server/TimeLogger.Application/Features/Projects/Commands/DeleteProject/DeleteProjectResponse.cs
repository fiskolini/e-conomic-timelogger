using System;

namespace TimeLogger.Application.Features.Projects.Commands.CreateProject
{
    public sealed class DeleteProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TimeAllocated { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public DateTimeOffset? Deadline { get; set; }
    }
}