using MediatR;

namespace TimeLogger.Application.Features.Times.Commands.Delete
{
    public class DeleteTimeCommand : IRequest<DeleteTimeResponse>
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
    }
}