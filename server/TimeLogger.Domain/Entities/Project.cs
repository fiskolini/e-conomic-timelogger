using System;
using TimeLogger.Domain.Common;

namespace TimeLogger.Domain.Entities
{
    public sealed class Project : BaseEntity
    {
        public string Name { get; set; }
        public DateTimeOffset CompletedAt { get; set; }
        public DateTimeOffset? Deadline { get; set; }
        public TimeSpan TimeAllocated { get; set; } = TimeSpan.Zero;
    }
}