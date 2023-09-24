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
        private readonly ITimeRepository _repository;
        private readonly IMapper _mapper;

        public GetTimesHandler(ITimeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PagedResults<GetTimesResponse>> Handle(GetTimesCommand command,
            CancellationToken cancellationToken)
        {
            var data = await _repository.GetAll(command, cancellationToken, command.ConsiderDeleted);
            var response = _mapper.Map<PagedResults<GetTimesResponse>>(data);

            return response;
        }
    }
}