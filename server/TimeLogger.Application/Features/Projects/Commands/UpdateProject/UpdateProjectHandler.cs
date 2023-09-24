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
            var entityToUpdate = await _projectRepository.GetSingle(command.Id, cancellationToken);

            if (!(entityToUpdate is { DateDeleted: null }))
            {
                throw new ItemNotFoundException(command.Id);
            }

            UpdateEntity(ref entityToUpdate, command);

            _projectRepository.Update(entityToUpdate);

            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<UpdateProjectResponse>(entityToUpdate);
        }

        /// <summary>
        /// Partially pdate entity with given Project Request
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
                        $"Project has been already marked as complete at '{entityToUpdate.CompletedAt}'"
                    );
                }

                entityToUpdate.CompletedAt = DateTimeOffset.Parse(command.CompletedAt);
            }

            if (command.Deadline != null)
            {
                entityToUpdate.Deadline = DateTime.Parse(command.Deadline);
            }
            else
            {
                entityToUpdate.Deadline = (DateTime?)null;
            }


            if (command.TimeAllocated != null)
            {
                entityToUpdate.TimeAllocated = (int)command.TimeAllocated;
            }
        }
    }
}