using MediatR;
using TimeLogger.Application.Common.Responses;

namespace TimeLogger.Application.Features.Times.Commands.Create
{
    public class CreateTimeResponse : BaseEntityResponse, IRequest<CreateTimeCommand>
    {
        public int Minutes { get; set; }
    }
}