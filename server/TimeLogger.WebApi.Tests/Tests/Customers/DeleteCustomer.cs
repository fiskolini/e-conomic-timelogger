using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TimeLogger.Api.Tests.TestFixtures;
using TimeLogger.Application.Features.Customers.Commands.Delete;
using Xunit;

namespace TimeLogger.Api.Tests.Tests.Customers
{
    [Collection("HTTPClientCollection")]
    public class DeleteCustomer
    {
        private readonly HttpClient _httpClient;

        public DeleteCustomer(HttpClientFixture fixture)
        {
            _httpClient = fixture.Client;
        }

        [Fact]
        public async Task DeleteCustomer_CustomerGetsDeleted()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var customerRequest = new DeleteCustomerCommand { Id = 3 };

            // Act
            var response = await _httpClient.DeleteAsync($"api/customers/{customerRequest.Id}", cancellationToken);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Fact]
        public async Task DeleteInvalidCustomer_CustomerNotFoundException_404()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var customerRequest = new DeleteCustomerCommand { Id = -1 };

            // Act
            var response = await _httpClient.DeleteAsync($"api/customers/{customerRequest.Id}", cancellationToken);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}