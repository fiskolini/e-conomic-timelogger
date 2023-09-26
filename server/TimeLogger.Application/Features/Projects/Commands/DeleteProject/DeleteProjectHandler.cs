using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions.Common;
using TimeLogger.Application.Features.Projects.Commands.CreateProject;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Application.Features.Projects.Commands.DeleteProject
{
    public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, DeleteProjectResponse>
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

        public async Task<DeleteProjectResponse> Handle(DeleteProjectCommand command,
            CancellationToken cancellationToken)
        {
            // Get the project to be deleted by its id
            var entityToDelete = await _projectRepository.GetSingle(command.Id, cancellationToken);

            // Check if the project has already been soft-deleted
            if (entityToDelete == null)
                throw new ItemNotFoundException(command.Id);

            // Set the DateDeleted property to the current UTC time
            entityToDelete.DateDeleted = DateTimeOffset.UtcNow;

            // Update the project in the repository
            _projectRepository.Update(entityToDelete);

            // Commit change
            await _unitOfWork.Commit(cancellationToken);

            // Map the deleted project to DeleteProjectResponse and return it
            return _mapper.Map<DeleteProjectResponse>(entityToDelete);
        }
    }
}