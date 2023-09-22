using System;
using TimeLogger.Application.Common.Responses;

namespace TimeLogger.Application.Features.Projects
{
    public class ProjectResponse : BaseEntityResponse
    {
        public string Name { get; set; }
        public int TimeAllocated { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public DateTimeOffset? Deadline { get; set; }
    }
}