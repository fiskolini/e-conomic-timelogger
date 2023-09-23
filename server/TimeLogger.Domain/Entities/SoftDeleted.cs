using System;

namespace TimeLogger.Domain.Entities
{
    public class SoftDeleted
    {
        public DateTimeOffset? DateDeleted { get; set; }
    }
}