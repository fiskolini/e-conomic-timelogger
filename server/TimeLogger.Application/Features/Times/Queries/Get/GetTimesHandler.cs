using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;

namespace TimeLogger.Application.Features.Times.Queries.Get
{
    public sealed class GetTimesHandler : IRequestHandler<GetTimesCommand, PagedResults<GetTimesResponse>>
    {
        private readonly ITimeRepository _timeRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public GetTimesHandler(ITimeRepository timeRepository, IProjectRepository projectRepository, IMapper mapper)
        {
            _timeRepository = timeRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<PagedResults<GetTimesResponse>> Handle(GetTimesCommand command,
            CancellationToken cancellationToken)
        {
            PagedResults<Time> times;
        
            // Create a paged results object to store the response
            var pagedResults = new PagedResults<GetTimesResponse>();
        
            // Check if a ProjectId filter is provided
            if (command.ProjectId != null)
            {
                var projectId = (int)command.ProjectId;
            
                // Get the single project
                var project = await _projectRepository.GetSingle(projectId, cancellationToken);

                if (project == null)
                {
                    // Return an empty response
                    pagedResults.Data = new List<GetTimesResponse>();
                    return pagedResults;
                }
            
                // Retrieve times related to the specified project
                times = await _timeRepository.GetAllByProjectId(projectId, command, cancellationToken,
                    command.ConsiderDeleted);
            }
            else
            {
                // Retrieve all times if no ProjectId filter is provided
                times = await _timeRepository.GetAll(command, cancellationToken, command.ConsiderDeleted);
            }

            return _mapper.Map<PagedResults<GetTimesResponse>>(times);
        }
    }
}