using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;

namespace TimeLogger.Application.Features.Times.Commands.Create
{
    public class CreateTimeHandler : IRequestHandler<CreateTimeCommand, CreateTimeResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeRepository _timeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public CreateTimeHandler(IUnitOfWork unitOfWork, ITimeRepository timeRepository,
            IProjectRepository projectRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _timeRepository = timeRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<CreateTimeResponse> Handle(CreateTimeCommand command,
            CancellationToken cancellationToken)
        {
            // Get project
            var project = await _projectRepository.GetSingle(command.ProjectId, cancellationToken, true);

            // Validate invalid project existing
            if (project == null)
                throw new NotFoundException($"Project {command.ProjectId} doesn't exist.");

            // Validate softDeleted item
            if (!(project is { DateDeleted: null }))
                throw new BadRequestException($"Project {command.ProjectId} has been deleted");

            // Validate already completed project
            if (project.CompletedAt != null)
                throw new BadRequestException($"Project {command.ProjectId} is already marked as complete.");

            // Map response
            var time = _mapper.Map<Time>(command);

            // Create new entity
            _timeRepository.Create(time);

            // Commit change
            await _unitOfWork.Commit(cancellationToken);

            return _mapper.Map<CreateTimeResponse>(time);
        }
    }
}