using System;
using System.ComponentModel.DataAnnotations.Schema;
using TimeLogger.Domain.Common;

namespace TimeLogger.Domain.Entities
{
    public sealed class Project : BaseEntity
    {
        public string Name { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public DateTime? Deadline { get; set; }
        public int TimeAllocated { get; set; }

        [ForeignKey("CustomerId")]
        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; }
    }
}