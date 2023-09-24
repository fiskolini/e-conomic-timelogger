using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Application.Features.Projects.Commands.CreateProject
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, CreateProjectResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public CreateProjectHandler(IUnitOfWork unitOfWork, IProjectRepository projectRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles project creation operation
        /// </summary>
        public async Task<CreateProjectResponse> Handle(CreateProjectCommand request,
            CancellationToken cancellationToken)
        {
            var project = _mapper.Map<Project>(request);
            _projectRepository.Create(project);
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateProjectResponse>(project);
        }
    }
}