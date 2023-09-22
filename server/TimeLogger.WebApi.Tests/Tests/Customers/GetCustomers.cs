using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TimeLogger.Api.Tests.TestFixtures;
using TimeLogger.Application.Features.Customers.Queries.Get;
using TimeLogger.Application.Features.Customers.Queries.GetById;
using TimeLogger.Domain.Entities;
using Xunit;

namespace TimeLogger.Api.Tests.Tests.Customers
{
    [Collection("HTTPClientCollection")]
    public class GetCustomers
    {
        private readonly HttpClient _httpClient;

        public GetCustomers(HttpClientFixture fixture)
        {
            _httpClient = fixture.Client;
        }

        [Fact]
        public async Task GetCustomer_PagedResults_ShouldReturnCorrectly()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var request = new GetCustomersRequest { PageSize = 2, PageNumber = 2 };

            // Set up the mediator to return a specific result when Send is called
            var expectedResponse = new PagedResults<GetCustomersResponse>
            {
                PageSize = request.PageSize,
                PageNumber = request.PageNumber
            };

            // Act
            var response = await _httpClient.GetAsync(
                $"api/customers/?pageNumber={request.PageNumber}&pageSize={request.PageSize}", cancellationToken);
            var returnedJson = await response.Content.ReadAsStringAsync();
            var returnedCustomer = JsonConvert.DeserializeObject<PagedResults<GetCustomersResponse>>(returnedJson);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedResponse.PageNumber, returnedCustomer.PageNumber);
            Assert.Equal(expectedResponse.PageSize, returnedCustomer.PageSize);
            Assert.Equal(expectedResponse.PageSize, returnedCustomer.Data.Count);
            Assert.True(returnedCustomer.Data[1].NumberOfProjects > 1);
        }

        [Fact]
        public async Task GetCustomer_HappyPath()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var customerRequest = new GetSingleCustomerCommand { Id = 2 };

            // Set up the mediator to return a specific result when Send is called
            var expectedResponse = new GetSingleCustomerResponse { Id = customerRequest.Id };

            // Act
            var response = await _httpClient.GetAsync($"api/customers/{customerRequest.Id}", cancellationToken);
            var returnedJson = await response.Content.ReadAsStringAsync();
            var returnedCustomer = JsonConvert.DeserializeObject<GetSingleCustomerResponse>(returnedJson);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedResponse.Id, returnedCustomer.Id);
            Assert.NotNull(returnedCustomer.Name);
            Assert.Null(returnedCustomer.DateDeleted);
            Assert.True(returnedCustomer.NumberOfProjects > 1);
        }

        [Fact]
        public async Task GetCustomer_CustomerNotFoundException_404()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var customerRequest = new GetSingleCustomerCommand { Id = -1 };

            // Act
            var response = await _httpClient.GetAsync($"api/customers/{customerRequest.Id}", cancellationToken);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}