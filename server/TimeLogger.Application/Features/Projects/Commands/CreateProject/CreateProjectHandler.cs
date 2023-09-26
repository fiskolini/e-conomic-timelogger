using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, CreateProjectResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectRepository _projectRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateProjectHandler(IUnitOfWork unitOfWork, IProjectRepository projectRepository,
            ICustomerRepository customerRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _projectRepository = projectRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles project creation operation
        /// </summary>
        public async Task<CreateProjectResponse> Handle(CreateProjectCommand request,
            CancellationToken cancellationToken)
        {
            // Validate customer existence
            var customer = await _customerRepository.GetSingle(request.CustomerId, cancellationToken);

            if (customer == null)
                throw new BadRequestException($"Customer {request.CustomerId} doesn't exist.");
            
            // Map command into entity response 
            var project = _mapper.Map<Project>(request);
            
            // Create project entity
            _projectRepository.Create(project);
            
            // Commit change
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateProjectResponse>(project);
        }
    }
}