using System;
using TimeLogger.Application.Features.ProjectFeatures.GetTime;

namespace TimeLogger.Application.Features.ProjectFeatures.GetProject
{
    public class GetProjectResponse
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public GetTimeResponse TimeAllocated { get; set; }
        public DateTimeOffset? Deadline { get; set; }
    }
}