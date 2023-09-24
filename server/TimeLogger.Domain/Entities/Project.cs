using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TimeLogger.Domain.Common;
using TimeLogger.Domain.Contracts;

namespace TimeLogger.Domain.Entities
{
    public sealed class Project : BaseEntity, INamedEntity
    {
        public string Name { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public DateTime? Deadline { get; set; }
        public int TimeAllocated { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; }
        
        public ICollection<Time> Times { get; set; }

    }
}