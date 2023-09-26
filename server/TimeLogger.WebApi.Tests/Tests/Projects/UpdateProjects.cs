using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TimeLogger.Api.Tests.TestFixtures;
using TimeLogger.Application.Features.Projects.Commands.CreateProject;
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
        public async Task UpdateProject_ProjectGetsUpdated()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var request = new UpdateProjectCommand { Id = 1, Name = "Foo" };
            var route = $"api/projects/{request.Id}";

            // Act
            var httpResponse = await _httpClient.PatchAsJsonAsync(route, request,
                cancellationToken);
            var responseAfterUpdate = await _httpClient.GetAsync(route, cancellationToken);
            var returnedJson = await responseAfterUpdate.Content.ReadAsStringAsync();
            var updatedProject = JsonConvert.DeserializeObject<UpdateProjectResponse>(returnedJson);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, httpResponse.StatusCode);
            Assert.Equal(request.Name, updatedProject.Name);
            Assert.NotNull(updatedProject.DateUpdated);
        }

        [Fact]
        public async Task UpdateProject_MarkingCompleteAlreadyCompletedProject_ErrorIsReturned()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var createProjectCommand = new CreateProjectCommand { CustomerId = 1, Name = "Foo" };
            var updateProjectCommand = new UpdateProjectCommand
                { CompletedAt = DateTime.Now.ToString(CultureInfo.InvariantCulture) };

            // Act

            // Create project
            var httpResponseCreateProject = await _httpClient.PostAsJsonAsync("api/projects/", createProjectCommand,
                cancellationToken);
            var createProjectJsonResponse = await httpResponseCreateProject.Content.ReadAsStringAsync();
            var createProjectResponse = JsonConvert.DeserializeObject<CreateProjectResponse>(createProjectJsonResponse);

            // Update project to mark is as complete
            var httpResponseUpdateProject =
                await _httpClient.PatchAsJsonAsync(
                    $"api/projects/{createProjectResponse.Id}",
                    updateProjectCommand,
                    cancellationToken);
            var updateProjectJsonResponse = await httpResponseCreateProject.Content.ReadAsStringAsync();
            var updateProjectResponse = JsonConvert.DeserializeObject<UpdateProjectResponse>(updateProjectJsonResponse);

            // Re-mark project as complete
            var httpResponseNewUpdateProject =
                await _httpClient.PatchAsJsonAsync($"api/projects/{createProjectResponse.Id}", updateProjectCommand,
                    cancellationToken);


            // Assert

            // No Content status for creating project
            Assert.Equal(HttpStatusCode.Created, httpResponseCreateProject.StatusCode);

            // Ok status for Updating project
            Assert.Equal(HttpStatusCode.NoContent, httpResponseUpdateProject.StatusCode);

            // Bad Request status while re-marking project as complete
            Assert.Equal(HttpStatusCode.BadRequest, httpResponseNewUpdateProject.StatusCode);
        }
    }
}