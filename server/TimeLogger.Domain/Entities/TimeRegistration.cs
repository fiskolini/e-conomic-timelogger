using System;
using TimeLogger.Domain.Common;

namespace TimeLogger.Domain.Entities
{
    public sealed class TimeRegistration : BaseEntity
    {
        public uint ProjectId { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}