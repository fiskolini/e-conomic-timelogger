using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;

namespace TimeLogger.Application.Features.Customers.Queries.Get
{
    public sealed class GetCustomersHandler : IRequestHandler<GetCustomersCommand, PagedResults<GetCustomersResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetCustomersHandler(ICustomerRepository customerRepository, IProjectRepository projectRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<PagedResults<GetCustomersResponse>> Handle(GetCustomersCommand command,
            CancellationToken cancellationToken)
        {
            // Fetch customer data based on the command and consideration for deleted items
            var data = await _customerRepository.GetAll(command, cancellationToken, command.ConsiderDeleted);
    
            // Map the retrieved customer data to GetCustomersResponse objects
            var response = _mapper.Map<PagedResults<GetCustomersResponse>>(data);
    
            // Extract customer IDs from the retrieved data
            var customerIds = data.Data.Select(c => c.Id).ToList();
    
            // Retrieve project counts for each customer
            var counts = await _projectRepository.GetCountForCustomers(
                customerIds, cancellationToken, command.ConsiderDeleted
            );

            // Iterate through the response data and update the 'NumberOfProjects' property
            foreach (var item in response.Data)
            {
                if (counts.TryGetValue(item.Id, out var count))
                {
                    item.NumberOfProjects = count;
                }
            }

            // Return the final response with customer data and project counts
            return response;
        }
    }
}