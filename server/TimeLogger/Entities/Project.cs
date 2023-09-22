using System;
using Microsoft.EntityFrameworkCore;

namespace TimeLogger.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool HasComplete { get; set; } = false;
        public DateTime? Deadline { get; set; }
        public DbSet<TimeRegistration> Times { get; set; }
    }
}