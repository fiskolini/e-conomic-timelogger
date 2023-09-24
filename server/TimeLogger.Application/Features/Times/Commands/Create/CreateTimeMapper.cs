using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Times.Commands.Create
{
    public class CreateTimeMapper : Profile
    {
        public CreateTimeMapper()
        {
            CreateMap<CreateTimeCommand, Time>();
            CreateMap<Time, CreateTimeResponse>();
        }
    }
}