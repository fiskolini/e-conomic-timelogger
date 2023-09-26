using System;
using TimeLogger.Application.Common.Responses;

namespace TimeLogger.Application.Features.Projects.Queries.GetById
{
    public class GetSingleProjectResponse : BaseEntityResponse
    {
        public string Name { get; set; }
        public int TimeAllocated { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public DateTimeOffset? Deadline { get; set; }
        public int CustomerId { get; set; }
    }
}