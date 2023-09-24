using MediatR;

namespace TimeLogger.Application.Features.Times.Commands.Create
{
    public class CreateTimeCommand : IRequest<CreateTimeResponse>
    {
        public int ProjectId { get; set; }
        public int Minutes { get; set; }
    }
}