using System;
using AutoMapper;

namespace TimeLogger.Application.Features.ProjectFeatures.GetTime
{
    public class GetProjectMapper : Profile
    {
        public GetProjectMapper()
        {
            CreateMap<TimeSpan, GetTimeResponse>();
        }
    }
}