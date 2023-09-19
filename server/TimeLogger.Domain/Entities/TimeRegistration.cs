using System;
using TimeLogger.Domain.Common;

namespace TimeLogger.Domain.Entities
{
    public sealed class TimeRegistration : BaseEntity
    {
        public uint ProjectId { get; set; }
        public Project Project { get; set; }
        public TimeSpan Time { get; set; }
    }
}