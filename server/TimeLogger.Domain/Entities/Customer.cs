using System.Collections.Generic;
using TimeLogger.Domain.Common;
using TimeLogger.Domain.Contracts;

namespace TimeLogger.Domain.Entities
{
    public sealed class Customer : BaseEntity, INamedEntity
    {
        public string Name { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}