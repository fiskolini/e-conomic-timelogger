using System;

namespace TimeLogger.Application.Common.Responses
{
    public abstract class BaseEntityResponse
    {
        public int Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset? DateUpdated { get; set; }
        public DateTimeOffset? DateDeleted { get; set; }
    }
}