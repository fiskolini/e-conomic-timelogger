using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions.Common;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, ProjectResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public DeleteProjectHandler(IUnitOfWork unitOfWork, IProjectRepository projectRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectResponse> Handle(DeleteProjectCommand command,
            CancellationToken cancellationToken)
        {
            var entityToDelete = await _projectRepository.GetSingle(command.Id, cancellationToken);

            if (!(entityToDelete is { DateDeleted: null }))
            {
                throw new ItemNotFoundException(command.Id);
            }

            entityToDelete.DateDeleted = DateTimeOffset.UtcNow;
            _projectRepository.Update(entityToDelete);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<ProjectResponse>(entityToDelete);
        }
    }
}