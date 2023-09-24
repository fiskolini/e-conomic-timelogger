using AutoMapper;
using TimeLogger.Domain.Entities;

namespace TimeLogger.Application.Features.Times.Commands.Delete
{
    public class DeleteTimeMapper : Profile
    {
        public DeleteTimeMapper()
        {
            CreateMap<DeleteTimeCommand, Time>();
            CreateMap<Time, DeleteTimeResponse>();
        }
    }
}