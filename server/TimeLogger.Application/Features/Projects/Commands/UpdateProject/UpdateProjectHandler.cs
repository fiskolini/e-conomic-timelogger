using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions;
using TimeLogger.Application.Common.Exceptions.Common;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, UpdateProjectResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public UpdateProjectHandler(IUnitOfWork unitOfWork, IProjectRepository projectRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<UpdateProjectResponse> Handle(UpdateProjectCommand command,
            CancellationToken cancellationToken)
        {
            // Get the project to be updated by its id
            var project = await _projectRepository.GetSingle(command.Id, cancellationToken);

            // Check if the project exists
            if (project == null)
                throw new ItemNotFoundException(command.Id);

            // Update the entity based on the command
            UpdateEntity(ref project, command);

            // Update the project in the repository
            _projectRepository.Update(project);

            // Commit change
            await _unitOfWork.Commit(cancellationToken);

            // Map the updated project to UpdateProjectResponse and return it
            return _mapper.Map<UpdateProjectResponse>(project);
        }

        /// <summary>
        /// Partially update entity with the given Project Request
        /// </summary>
        private static void UpdateEntity(ref Project entityToUpdate, UpdateProjectCommand command)
        {
            if (command.Name != null)
            {
                entityToUpdate.Name = command.Name;
            }

            if (command.CompletedAt != null)
            {
                // Prevent marking as complete a project already completed
                if (entityToUpdate.CompletedAt.HasValue)
                {
                    throw new BadRequestException(
                        $"Project has already been marked as complete at '{entityToUpdate.CompletedAt}'"
                    );
                }

                entityToUpdate.CompletedAt = DateTimeOffset.Parse(command.CompletedAt);
            }

            if (string.IsNullOrEmpty(command.Deadline)) return;
            
            // If the user is trying to update, then we have to set its value
            if (entityToUpdate.Deadline == null)
            {
                entityToUpdate.Deadline = DateTime.Parse(command.Deadline);
            }
            else
            {
                entityToUpdate.Deadline = null;
            }
        }
    }
}