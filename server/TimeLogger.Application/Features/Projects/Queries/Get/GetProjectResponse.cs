using System;
using TimeLogger.Application.Common.Responses;

namespace TimeLogger.Application.Features.Projects.Queries.Get
{
    public class GetProjectResponse : BaseEntityResponse
    {
        public string Name { get; set; }
        public int TimeAllocated { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public DateTimeOffset? Deadline { get; set; }
    }
}