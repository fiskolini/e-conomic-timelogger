using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;
using TimeLogger.Persistence.Context;
using TimeLogger.Persistence.Repositories;
using TimeLogger.Persistence.Repositories.Common;
using TimeLogger.Persistence.Seeders;

namespace TimeLogger.Persistence.Common
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseInMemoryDatabase("e-conomic interview");
                // Ignore transaction warnings
                opt.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITimeRepository, TimeRepository>();
        }

        public static void UsePersistence(this IApplicationBuilder app)
        {
            var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var databaseSeeder = new DatabaseSeeder(scope);
                databaseSeeder.Seed();
            }
        }
    }
}