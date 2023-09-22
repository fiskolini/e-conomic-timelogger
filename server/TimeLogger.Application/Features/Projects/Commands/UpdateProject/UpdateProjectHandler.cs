using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions.Common;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Application.Features.Projects.Commands.UpdateProject
{
    public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, ProjectResponse>
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

        public async Task<ProjectResponse> Handle(UpdateProjectCommand command,
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

            return _mapper.Map<ProjectResponse>(entityToUpdate);
        }

        /// <summary>
        /// Partially pdate entity with given Project Request
        /// </summary>
        private void UpdateEntity(ref Project entityToUpdate, UpdateProjectCommand command)
        {
            if (command.Name != null)
            {
                entityToUpdate.Name = command.Name;
            }

            if (command.CompletedAt != null)
            {
                entityToUpdate.CompletedAt = DateTimeOffset.Parse(command.CompletedAt);
            }

            if (command.Deadline != null)
            {
                entityToUpdate.Deadline = DateTime.Parse(command.Deadline);
            }

            if (command.TimeAllocated != null)
            {
                entityToUpdate.TimeAllocated = (int)command.TimeAllocated;
            }
        }
    }
}