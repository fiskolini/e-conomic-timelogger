using System;
using TimeLogger.Domain.Contracts;

namespace TimeLogger.Domain.Common
{
    public abstract class BaseEntity : IIdentifiableEntity
    {
        public int Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
    }
}