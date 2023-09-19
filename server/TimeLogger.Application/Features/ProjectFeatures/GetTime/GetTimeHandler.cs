using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace TimeLogger.Application.Features.ProjectFeatures.GetTime
{
    public sealed class
        GetTimeHandler : IRequestHandler<GetTimeRequest, GetTimeResponse>
    {
        private readonly IMapper _mapper;

        public GetTimeHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<GetTimeResponse> Handle(GetTimeRequest request,
            CancellationToken cancellationToken)
        {
            var timeSpan = request.TimeSpanValue;
            var timeSpanDto = new GetTimeResponse
            {
                Hours = timeSpan.Hours,
                Minutes = timeSpan.Minutes,
                Seconds = timeSpan.Seconds
            };

            return Task.FromResult(timeSpanDto);
        }
    }
}