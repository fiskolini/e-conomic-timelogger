using TimeLogger.Application.Common.Responses;

namespace TimeLogger.Application.Features.Times.Queries.Get
{
    public class GetTimesResponse : BaseEntityResponse
    {
        public string Name { get; set; }
        public int NumberOfProjects { get; set; }
        public int TotalTimeAllocated { get; set; }
    }
}