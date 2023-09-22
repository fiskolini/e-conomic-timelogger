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
        private IServiceScope _scope;

        public DatabaseSeeder(IServiceScope scope)
        {
            _scope = scope;
        }

        /// <summary>
        /// Seed database by adding random data into it
        /// </summary>
        public void Seed()
        {
            using (var context = _scope.ServiceProvider.GetService<DataContext>())
            {
                var customerRepository = _scope.ServiceProvider.GetService<ICustomerRepository>();

                var faker = new Faker();

                var customers = new List<Customer>();
                customers.Add(new Customer
                {
                    Name = "e-conomic"
                });
                
                for (int i = 1; i <= 15; i++)
                {
                    var customer = new Customer
                    {
                        Name = faker.Company.CompanyName()
                    };
                    customers.Add(customer);

                    var projects = new List<Project>();
                    for (int p = 1; p <= 5; p++)
                    {
                        var project = new Project
                        {
                            Name = faker.Person.FirstName,
                            Deadline = faker.Date.Future(),
                            TimeAllocated = 90
                        };
                        projects.Add(project);
                    }

                    customer.Projects = projects; // Set the projects for the customer
                }

                customerRepository.CreateRange(customers);

                context.SaveChanges();
            }
        }
    }
}