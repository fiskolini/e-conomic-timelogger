using TimeLogger.Application.Common.Responses;

namespace TimeLogger.Application.Features.Times.Queries.Get
{
    public class GetTimesResponse : BaseEntityResponse
    {
        public int ProjectId { get; set; }
        public int Minutes { get; set; }
    }
}