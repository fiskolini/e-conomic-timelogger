using TimeLogger.Application.Common.Responses;

namespace TimeLogger.Application.Features.Times.Queries.GetById
{
    public class GetSingleTimeResponse : BaseEntityResponse
    {
        public string Name { get; set; }
        public int NumberOfProjects { get; set; }
        public int TotalTimeAllocated { get; set; }
    }
}