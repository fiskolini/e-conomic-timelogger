using System;
using System.Collections.Generic;
using TimeLogger.Domain.Common;

namespace TimeLogger.Domain.Entities
{
    public sealed class Project : BaseEntity
    {
        public string Name { get; set; }
        public bool HasComplete { get; set; } = false;
        public DateTimeOffset? Deadline { get; set; }
        public List<TimeRegistration> Times { get; set; }
    }
}