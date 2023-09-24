using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TimeLogger.Api.Tests.TestFixtures;
using TimeLogger.Application.Features.Customers.Commands.Update;
using Xunit;

namespace TimeLogger.Api.Tests.Tests.Customers
{
    [Collection("HTTPClientCollection")]
    public class UpdateCustomers
    {
        private readonly HttpClient _httpClient;

        public UpdateCustomers(HttpClientFixture fixture)
        {
            _httpClient = fixture.Client;
        }

        [Fact]
        public async Task UpdateCustomer_CustomerGetsUpdated()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var customerRequest = new UpdateCustomerCommand { Id = 1, Name = "Foo" };

            // Act
            var response = await _httpClient.PatchAsJsonAsync($"api/customers/{customerRequest.Id}", customerRequest,
                cancellationToken);
            var responseAfterUpdate = await _httpClient.GetAsync($"api/customers/{customerRequest.Id}", cancellationToken);
            var returnedJson = await responseAfterUpdate.Content.ReadAsStringAsync();
            var updatedCustomer = JsonConvert.DeserializeObject<UpdateCustomerResponse>(returnedJson);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
            Assert.Equal(customerRequest.Name, updatedCustomer.Name);
            Assert.NotNull(updatedCustomer.DateUpdated);
        }
    }
}