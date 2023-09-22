using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions.Common;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;

namespace TimeLogger.Application.Features.Projects.Queries.Get
{
    public sealed class GetProjectHandler : IRequestHandler<GetProjectCommand, PagedResults<GetProjectResponse>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectHandler(ICustomerRepository customerRepository, IProjectRepository projectRepository,
            IMapper mapper)
        {
            _customerRepository = customerRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<PagedResults<GetProjectResponse>> Handle(GetProjectCommand command,
            CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.GetSingle(command.CustomerId, cancellationToken);

            if (customer == null)
            {
                throw new ItemNotFoundException(command.CustomerId);
            }

            var projects = await _projectRepository.GetAllByCustomerId(command.CustomerId, command, cancellationToken,
                command.ConsiderDeleted);
            
            return _mapper.Map<PagedResults<GetProjectResponse>>(projects);
        }
    }
}