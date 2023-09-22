using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions.Common;
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
            var customer = await _repository.GetSingle(command.Id, cancellationToken, command.ConsiderDeleted);

            if (customer == null)
            {
                throw new ItemNotFoundException(command.Id);
            }

            var response = _mapper.Map<GetSingleCustomerResponse>(customer);

            var count = await _repository.GetProjectsCount(customer.Id, cancellationToken, command.ConsiderDeleted);

            response.NumberOfProjects = count;

            return response;
        }
    }
}