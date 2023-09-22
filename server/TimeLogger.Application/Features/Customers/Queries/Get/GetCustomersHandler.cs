using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;

namespace TimeLogger.Application.Features.Customers.Queries.Get
{
    public sealed class GetCustomersHandler : IRequestHandler<GetCustomersRequest, PagedResults<GetCustomersResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomersHandler(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<PagedResults<GetCustomersResponse>> Handle(GetCustomersRequest request, CancellationToken cancellationToken)
        {
            var data = await _customerRepository.GetAll(request, cancellationToken, request.ConsiderDeleted);
            var response = _mapper.Map<PagedResults<GetCustomersResponse>>(data);
            var counts = await _customerRepository.GetProjectsCounts(data.Data.Select(c => c.Id).ToList(),
                cancellationToken, request.ConsiderDeleted);

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