using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Domain.Repositories;

namespace TimeLogger.Application.Features.Customers.Queries.GetById
{
    public class GetSingleCustomerHandler : IRequestHandler<GetSingleCustomerCommand, GetSingleCustomerResponse>
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public GetSingleCustomerHandler(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetSingleCustomerResponse> Handle(GetSingleCustomerCommand command,
            CancellationToken cancellationToken)
        {
            // Retrieve a single customer based on the provided customer ID and consideration for deleted items
            var customer = await _repository.GetSingle(command.Id, cancellationToken, command.ConsiderDeleted);

            // Map the retrieved customer data to a GetSingleCustomerResponse object
            var response = _mapper.Map<GetSingleCustomerResponse>(customer);

            // If no customer is found, return null as there is no data to process
            if (customer == null)
                return null;

            // Retrieve the project count for the customer
            var count = await _repository.GetProjectsCount(customer.Id, cancellationToken, command.ConsiderDeleted);

            // Update the 'NumberOfProjects' property in the response with the project count
            response.NumberOfProjects = count;

            return response;
        }
    }
}