using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;

namespace TimeLogger.Application.Features.Projects.Queries.Get
{
    public sealed class GetProjectsHandler : IRequestHandler<GetProjectsCommand, PagedResults<GetProjectsResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly ITimeRepository _timeRepository;
        private readonly IMapper _mapper;

        public GetProjectsHandler(ICustomerRepository customerRepository, IProjectRepository projectRepository, ITimeRepository timeRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _projectRepository = projectRepository;
            _timeRepository = timeRepository;
            _mapper = mapper;
        }

        public async Task<PagedResults<GetProjectsResponse>> Handle(GetProjectsCommand command,
            CancellationToken cancellationToken)
        {
            PagedResults<Project> projects;

            if (command.CustomerId != null)
            {
                // Extract the customerId from the command
                var customerId = (int)command.CustomerId;

                // Get the customer by customerId
                var customer = await _customerRepository.GetSingle(customerId, cancellationToken);

                // If no customer is found, return an empty response
                if (customer == null)
                {
                    return new PagedResults<GetProjectsResponse>
                    {
                        Data = new List<GetProjectsResponse>(),
                        TotalItems = 0,
                        PageNumber = 0
                    };
                }

                // Get projects for the specified customer
                projects = await _projectRepository.GetAllByCustomerId(customerId, command, cancellationToken,
                    command.ConsiderDeleted);
            }
            else
            {
                // Get all projects if no customerId filter is specified
                projects = await _projectRepository.GetAll(command, cancellationToken, command.ConsiderDeleted);
            }

            // Map the projects to GetProjectsResponse objects
            var response = _mapper.Map<PagedResults<GetProjectsResponse>>(projects);

            // Extract projectIds from the retrieved projects
            var projectIds = projects.Data.Select(c => c.Id).ToList();

            // Get the total allocated time in minutes for each project
            var counts = await _timeRepository.GetTotalMinutesPerProject(projectIds, cancellationToken,
                command.ConsiderDeleted
            );

            // Update the TimeAllocated property in the response
            foreach (var item in response.Data)
            {
                if (counts.TryGetValue(item.Id, out var sum))
                    item.TimeAllocated = sum;
            }

            return response;
        }
    }
}