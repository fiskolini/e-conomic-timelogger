using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TimeLogger.Api.Tests.TestFixtures;
using TimeLogger.Application.Features.Customers.Queries.Get;
using TimeLogger.Application.Features.Projects.Queries.Get;
using TimeLogger.Application.Features.Projects.Queries.GetById;
using TimeLogger.Domain.Entities;
using Xunit;

namespace TimeLogger.Api.Tests.Tests.Projects
{
    [Collection("HTTPClientCollection")]
    public class GetProjects
    {
        private readonly HttpClient _httpClient;

        public GetProjects(HttpClientFixture fixture)
        {
            _httpClient = fixture.Client;
        }

        [Fact]
        public async Task GetProject_HappyPath()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var projectRequest = new GetSingleProjectCommand { Id = 2, CustomerId = 2 };

            // Set up the expected result being returned
            var expectedResponse = new GetSingleProjectResponse { Id = projectRequest.Id };

            // Act
            var response = await _httpClient.GetAsync(
                $"api/projects/{projectRequest.Id}?customerId={projectRequest.CustomerId}", cancellationToken);
            var returnedJson = await response.Content.ReadAsStringAsync();
            var returnedProject = JsonConvert.DeserializeObject<GetSingleProjectResponse>(returnedJson);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedResponse.Id, returnedProject.Id);
        }

        [Fact]
        public async Task GetProject_PagedResults_ShouldReturnCorrectly()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var request = new GetProjectsCommand { CustomerId = 2, PageSize = 2, PageNumber = 2 };

            // Set up the expected result being returned
            var expectedResponse = new PagedResults<GetProjectsResponse>
            {
                PageSize = request.PageSize,
                PageNumber = request.PageNumber
            };

            // Act
            var response = await _httpClient.GetAsync(
                $"api/projects/?customerId={request.CustomerId}&pageNumber={request.PageNumber}&pageSize={request.PageSize}",
                cancellationToken);
            var returnedJson = await response.Content.ReadAsStringAsync();
            var returnedCustomer = JsonConvert.DeserializeObject<PagedResults<GetProjectsResponse>>(returnedJson);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(expectedResponse.PageNumber, returnedCustomer.PageNumber);
            Assert.Equal(expectedResponse.PageSize, returnedCustomer.PageSize);
            Assert.Equal(expectedResponse.PageSize, returnedCustomer.Data.Count);
        }

        [Fact]
        public async Task GetProject_PagedResults_ShouldReturnBadRequestOnGiganticLimit()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var request = new GetProjectsCommand { CustomerId = 2, PageSize = 501 };

            // Act
            var response = await _httpClient.GetAsync(
                $"api/projects?customerId={request.CustomerId}&pageSize={request.PageSize}", cancellationToken);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetCustomer_FilterBySearch_ShouldReturnCorrectly()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var request = new GetProjectsCommand { Search = "a", CustomerId = 2 };

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
        public async Task GetProject_CustomerNotFoundException_404()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var projectRequest = new GetSingleProjectCommand { CustomerId = 99, Id = -1 };

            // Act
            var response =
                await _httpClient.GetAsync($"api/customers/{projectRequest.CustomerId}/projects/{projectRequest.Id}",
                    cancellationToken);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}