using System;
using MediatR;

namespace TimeLogger.Application.Features.ProjectFeatures.GetTime
{
    public sealed class GetTimeRequest : IRequest<GetTimeResponse>
    {
        public TimeSpan TimeSpanValue { get; }

        public GetTimeRequest(TimeSpan timeSpanValue)
        {
            TimeSpanValue = timeSpanValue;
        }
    }
}