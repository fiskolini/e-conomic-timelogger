using System;

namespace TimeLogger.Domain.Contracts
{
    public interface ISoftDeletedEntity
    {
        public DateTimeOffset? DateDeleted { get; set; }
    }
}