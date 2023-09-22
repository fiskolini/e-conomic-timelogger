using System.Collections.Generic;
using TimeLogger.Domain.Common;

namespace TimeLogger.Domain.Entities
{
    public sealed class Customer : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}