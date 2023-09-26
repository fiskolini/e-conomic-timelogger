using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions;
using TimeLogger.Domain.Repositories;

namespace TimeLogger.Application.Features.Projects.Queries.GetById
{
    public class GetSingleProjectHandler : IRequestHandler<GetSingleProjectCommand, GetSingleProjectResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetSingleProjectHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<GetSingleProjectResponse> Handle(GetSingleProjectCommand command,
            CancellationToken cancellationToken)
        {
            // Check if CustomerId filter is provided
            if (command.CustomerId != null)
            {
                var customerId = (int)command.CustomerId;

                // Get the project associated with the specified customer id and project id
                var projectFromCustomer = await _projectRepository.GetSingle(customerId, command.Id,
                    cancellationToken, command.ConsiderDeleted);

                // If no project is found, throw a BadRequestException
                if (projectFromCustomer == null)
                {
                    throw new BadRequestException($"Customer {command.CustomerId} doesn't exist.");
                }

                // Map the project to GetSingleProjectResponse and return it
                return _mapper.Map<GetSingleProjectResponse>(projectFromCustomer);
            }

            // Get the project by project id
            var project = await _projectRepository.GetSingle(command.Id, cancellationToken, command.ConsiderDeleted);

            // Map the project to GetSingleProjectResponse and return it
            return _mapper.Map<GetSingleProjectResponse>(project);
        }
    }
}