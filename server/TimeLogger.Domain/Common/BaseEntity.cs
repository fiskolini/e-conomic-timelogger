using System;

namespace TimeLogger.Domain.Common
{
    public abstract class BaseEntity
    {
        public uint Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
    }
}