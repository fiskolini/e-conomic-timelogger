using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Times.Commands.Update
{
    public class UpdateTimeMapper : Profile
    {
        public UpdateTimeMapper()
        {
            CreateMap<UpdateTimeCommand, Time>();
            CreateMap<Time, UpdateTimeResponse>();
        }
    }
}