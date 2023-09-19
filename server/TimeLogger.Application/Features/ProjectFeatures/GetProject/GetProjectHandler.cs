using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Repositories.Contracts;

namespace TimeLogger.Application.Features.ProjectFeatures.GetProject
{
    public sealed class
        GetProjectHandler : IRequestHandler<GetProjectRequest, PagedResponse<GetProjectResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetProjectHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<PagedResponse<GetProjectResponse>> Handle(GetProjectRequest request,
            CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.GetAll(request, cancellationToken);
            return _mapper.Map<PagedResponse<GetProjectResponse>>(projects);
        }
    }
}