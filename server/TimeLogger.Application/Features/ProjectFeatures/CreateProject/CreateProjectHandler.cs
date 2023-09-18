using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Repositories.Contracts;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.ProjectFeatures.CreateProject
{
    public class CreateProjectHandler : IRequestHandler<CreateProjectRequest, CreateProjectResponse>
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
        public async Task<CreateProjectResponse> Handle(CreateProjectRequest request,
            CancellationToken cancellationToken)
        {
            var user = _mapper.Map<Project>(request);
            _projectRepository.Create(user);
            await _unitOfWork.Save(cancellationToken);

            return _mapper.Map<CreateProjectResponse>(user);
        }
    }
}