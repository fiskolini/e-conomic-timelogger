using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Application.Repositories.Contracts;

namespace TimeLogger.Application.Features.ProjectFeatures.GetAllProject
{
    public sealed class GetAllProjectHandler : IRequestHandler<GetAllProjectRequest, List<GetAllProjectResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetAllProjectHandler(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllProjectResponse>> Handle(GetAllProjectRequest request,
            CancellationToken cancellationToken)
        {
            var users = await _projectRepository.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllProjectResponse>>(users);
        }
    }
}