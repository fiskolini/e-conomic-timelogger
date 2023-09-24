using System;
using System.Collections.Generic;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Persistence.Context;

namespace TimeLogger.Persistence.Seeders
{
    public class DatabaseSeeder
    {
        private readonly IServiceScope _scope;

        public DatabaseSeeder(IServiceScope scope)
        {
            _scope = scope;
        }

        /// <summary>
        /// Seed database by adding random data into it
        /// </summary>
        public void Seed()
        {
            using var context = _scope.ServiceProvider.GetService<DataContext>();
            var customerRepository = _scope.ServiceProvider.GetService<ICustomerRepository>();
            var projectRepository = _scope.ServiceProvider.GetService<IProjectRepository>();
            var customerFaker = new Faker();
            var customers = new List<Customer>
            {
                // Add first brand new customer
                new Customer
                {
                    Name = "e-conomic"
                }
            };

            for (var i = 1; i <= 15; i++)
            {
                var projects = new List<Project>();
                var customer = new Customer
                {
                    Name = customerFaker.Company.CompanyName()
                };

                // Attach random picked customer
                customers.Add(customer);


                // You gotta think "why is this Random().Next(10) + 5 needed for"?
                // Well... I want to seed DB with as much as items possible having count > 10,
                // as I really want to try the app with paginated results... But not on all them :)
                for (var p = 1; p <= new Random().Next(10) + 5; p++)
                {
                    var projectFaker = new Faker();
                    int[] times = { 0, 30, 35, 50, 120, 200, 300, 5000 };
                    var random = new Random();
                    var arrayLength = times.Length;
                    var randomIndex = random.Next(arrayLength);
                    DateTime? deadline = null;
                    DateTime? completed = null;

                    // Set random deadline for SOME projects, not all
                    if (random.Next(3) == 1)
                    {
                        // Just set a few as "delayed"
                        deadline = random.Next(5) > 1 ? projectFaker.Date.Future() : projectFaker.Date.Past();
                    }
                    
                    // Set random completed for almost every project 
                    if (random.Next(6) > 1)
                    {
                        // Just set a few as "delayed"
                        completed = DateTime.Today;
                    }

                    // Create random project
                    var project = new Project
                    {
                        Name = $"{projectFaker.Person.FirstName} {projectFaker.Person.LastName}",
                        Deadline = deadline,
                        TimeAllocated = times[randomIndex],
                        CompletedAt = completed
                    };

                    // Attach projects to the client
                    projects.Add(project);
                }

                customer.Projects = projects;
                projectRepository.CreateRange(projects);
                customerRepository.CreateRange(customers);
            }

            context.SaveChanges();
        }
    }
}