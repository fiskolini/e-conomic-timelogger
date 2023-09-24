using MediatR;
using TimeLogger.Application.Common.Commands;

namespace TimeLogger.Application.Features.Times.Queries.GetById
{
    public class GetSingleTimeCommand : BaseEntityCommand, IRequest<GetSingleTimeResponse>
    {
        public int ProjectId { get; set; }
        public bool ConsiderDeleted { get; set; } = false;
    }
}