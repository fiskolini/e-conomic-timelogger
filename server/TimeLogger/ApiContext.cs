using Microsoft.EntityFrameworkCore;
using TimeLogger.Entities;

namespace TimeLogger
{
	public class ApiContext : DbContext
	{
		public ApiContext(DbContextOptions<ApiContext> options)
			: base(options)
		{
		}

		public DbSet<Project> Projects { get; set; }
	}
}
