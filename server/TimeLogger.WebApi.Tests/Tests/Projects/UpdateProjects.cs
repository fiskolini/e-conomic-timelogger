using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TimeLogger.Api.Tests.TestFixtures;
using TimeLogger.Application.Features.Customers.Commands.Update;
using TimeLogger.Application.Features.Projects.Commands.UpdateProject;
using Xunit;

namespace TimeLogger.Api.Tests.Tests.Projects
{
    [Collection("HTTPClientCollection")]
    public class UpdateProjects
    {
        private readonly HttpClient _httpClient;

        public UpdateProjects(HttpClientFixture fixture)
        {
            _httpClient = fixture.Client;
        }

        [Fact]
        public async Task UpdateProject_CustomerGetsUpdated()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var request = new UpdateProjectCommand { CustomerId = 2, Id = 1, Name = "Foo" };

            // Act
            var httpResponse = await _httpClient.PatchAsJsonAsync($"api/customers/{request.CustomerId}/projects/{request.Id}", request,
                cancellationToken);
            var responseAfterUpdate = await _httpClient.GetAsync($"api/customers/{request.CustomerId}/projects/{request.Id}", cancellationToken);
            var returnedJson = await responseAfterUpdate.Content.ReadAsStringAsync();
            var updatedProject = JsonConvert.DeserializeObject<UpdateProjectResponse>(returnedJson);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);
            Assert.Equal(request.Name, updatedProject.Name);
            Assert.NotNull(updatedProject.DateUpdated);
        }
        
        // TODO prevent marking as complete a project already completed
        
    }
}