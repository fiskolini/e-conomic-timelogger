using Microsoft.EntityFrameworkCore;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Persistence.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Time> Times { get; set; }
    }
}