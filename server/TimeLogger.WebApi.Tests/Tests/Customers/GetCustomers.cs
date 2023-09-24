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
        public async Task GetCustomer_HappyPath()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var customerRequest = new GetSingleCustomerCommand { Id = 2 };

            // Set up the expected result being returned
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
        public async Task GetCustomer_PagedResults_ShouldReturnCorrectly()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var request = new GetCustomersCommand { PageSize = 2, PageNumber = 2 };

            // Set up the expected result being returned
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
        public async Task GetCustomer_PagedResults_ShouldReturnBadRequestOnGiganticLimit()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var request = new GetCustomersCommand { PageSize = 501 };

            // Act
            var response = await _httpClient.GetAsync($"api/customers/?pageSize={request.PageSize}", cancellationToken);
            
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetCustomer_FilterBySearch_ShouldReturnCorrectly()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var request = new PagedRequest { Search = "conomic" };

            // Act
            var response = await _httpClient.GetAsync(
                $"api/customers/?search={request.Search}", cancellationToken);
            var returnedJson = await response.Content.ReadAsStringAsync();
            var returnedCustomer = JsonConvert.DeserializeObject<PagedResults<GetCustomersResponse>>(returnedJson);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(returnedCustomer.Data.Count >= 1);
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