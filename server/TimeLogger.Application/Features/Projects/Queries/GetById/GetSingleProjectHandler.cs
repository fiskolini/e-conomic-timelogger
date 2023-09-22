using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Common.Exceptions.Common;
using TimeLogger.Domain.Repositories;

namespace TimeLogger.Application.Features.Projects.Queries.GetById
{
    public class GetSingleProjectHandler : IRequestHandler<GetSingleProjectCommand, ProjectResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetSingleProjectHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<ProjectResponse> Handle(GetSingleProjectCommand command,
            CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetSingle(command.Id, cancellationToken, command.ConsiderDeleted);

            if (project == null)
            {
                throw new ItemNotFoundException(command.Id);
            }

            return _mapper.Map<ProjectResponse>(project);
        }
    }
}