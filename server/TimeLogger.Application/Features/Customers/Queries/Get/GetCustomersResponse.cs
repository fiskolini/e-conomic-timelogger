using TimeLogger.Application.Common.Responses;

namespace TimeLogger.Application.Features.Customers.Queries.Get
{
    public class GetCustomersResponse : BaseEntityResponse
    {
        public string Name { get; set; }
        public int NumberOfProjects { get; set; }
    }
}