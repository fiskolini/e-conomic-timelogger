using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TimeLogger.Application.Repositories.Contracts;
using TimeLogger.Domain.Entities;
using TimeLogger.Persistence.Context;
using TimeLogger.Persistence.Repositories;

namespace TimeLogger.Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("e-conomic interview"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
        }

        public static void UsePersistence(this IApplicationBuilder app)
        {
            var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                SeedDatabase(scope);
            }
        }


        private static void SeedDatabase(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<DataContext>();
            var testProject1 = new Project
            {
                Id = 1,
                Name = "e-conomic Interview",
                TimeAllocated = TimeSpan.FromMinutes(22),
                DateCreated = new DateTimeOffset(),
                Deadline = DateTimeOffset.UtcNow.AddDays(-2)
            };

            context.Projects.Add(testProject1);

            context.SaveChanges();
        }
    }
}