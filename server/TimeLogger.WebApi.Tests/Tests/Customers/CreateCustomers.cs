using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TimeLogger.Api.Tests.TestFixtures;
using TimeLogger.Application.Features.Customers.Commands.Create;
using Xunit;

namespace TimeLogger.Api.Tests.Tests.Customers
{
    [Collection("HTTPClientCollection")]
    public class CreateCustomers
    {
        private readonly HttpClient _httpClient;

        public CreateCustomers(HttpClientFixture fixture)
        {
            _httpClient = fixture.Client;
        }

        [Fact]
        public async Task CreateCustomer_CustomerGetsCreated()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var customerRequest = new CreateCustomerCommand { Name = "Foo" };

            // Act
            var response = await _httpClient.PostAsJsonAsync($"api/customers/", customerRequest, cancellationToken);
            var returnedJson = await response.Content.ReadAsStringAsync();
            var returnedCustomer = JsonConvert.DeserializeObject<CreateCustomerResponse>(returnedJson);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Headers.Location);
            Assert.Equal(returnedCustomer.DateCreated.Date, DateTime.Today);
            Assert.EndsWith($"api/customers/{returnedCustomer.Id}", response.Headers.Location.AbsoluteUri);
        }

        [Fact]
        public async Task CreateCustomerWithInvalidName_ReturnsBadRequest()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var customerRequest = new CreateCustomerCommand();

            // Act
            var response = await _httpClient.PostAsJsonAsync($"api/customers/", customerRequest, cancellationToken);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}