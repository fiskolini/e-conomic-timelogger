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
            var data = await _customerRepository.GetAll(command, cancellationToken, command.ConsiderDeleted);
            var response = _mapper.Map<PagedResults<GetCustomersResponse>>(data);
            var customerIds = data.Data.Select(c => c.Id).ToList();
            var counts = await _projectRepository.GetCountForCustomers(
                customerIds, cancellationToken, command.ConsiderDeleted
            );

            foreach (var item in response.Data)
            {
                if (counts.TryGetValue(item.Id, out var count))
                {
                    item.NumberOfProjects = count;
                }
            }

            return response;
        }
    }
}