using TimeLogger.Application.Common.Responses;

namespace TimeLogger.Application.Features.Customers.Queries.GetById
{
    public class GetSingleCustomerResponse : BaseEntityResponse
    {
        public string Name { get; set; }
        public int NumberOfProjects { get; set; }
        public int TotalTimeAllocated { get; set; }
    }
}