using MediatR;

namespace TimeLogger.Application.Features.Times.Commands.Update
{
    public class UpdateTimeCommand : IRequest<UpdateTimeResponse>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int Minutes { get; set; }
    }
}