using MediatR;
using TimeLogger.Application.Common.Responses;

namespace TimeLogger.Application.Features.Times.Commands.Update
{
    public class UpdateTimeResponse : BaseEntityResponse, IRequest<UpdateTimeCommand>
    {
        public string Name { get; set; }
    }
}