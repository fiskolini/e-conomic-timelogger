using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TimeLogger.Domain.Repositories;

namespace TimeLogger.Application.Features.Times.Queries.GetById
{
    public class GetSingleTimeHandler : IRequestHandler<GetSingleTimeCommand, GetSingleTimeResponse>
    {
        private readonly ITimeRepository _repository;
        private readonly IMapper _mapper;

        public GetSingleTimeHandler(ITimeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetSingleTimeResponse> Handle(GetSingleTimeCommand command,
            CancellationToken cancellationToken)
        {
            // Retrieve the time entity by ID
            var time = await _repository.GetSingle(command.Id, cancellationToken, command.ConsiderDeleted);

            // Map the time entity to the response object
            var response = _mapper.Map<GetSingleTimeResponse>(time);

            return response;
        }
    }
}