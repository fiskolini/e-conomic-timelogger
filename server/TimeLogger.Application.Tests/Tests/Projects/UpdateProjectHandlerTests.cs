using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TimeLogger.Application.Common.Exceptions;
using TimeLogger.Application.Features.Projects.Commands.UpdateProject;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;
using Xunit;

namespace TimeLogger.Application.Tests.Tests.Projects
{
    public class UpdateProjectHandlerTests
    {
        [Fact]
        public async Task Handle_CompletedAtSet_ThrowsBadRequestException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            var projectRepositoryMock = new Mock<IProjectRepository>();
            var mapperMock = new Mock<IMapper>();

            var handler = new UpdateProjectHandler(unitOfWorkMock.Object, projectRepositoryMock.Object, mapperMock.Object);

            var command = new UpdateProjectCommand
            {
                Id = 1,
                CompletedAt = "2023-09-13T12:00:00"
            };

            var projectToUpdate = new Project
            {
                Id = command.Id,
                // Include other properties as needed
                CompletedAt = DateTimeOffset.UtcNow // Simulate an already completed project
            };

            projectRepositoryMock.Setup(repo => repo.GetSingle(It.IsAny<int>(), It.IsAny<CancellationToken>(), false))
                .ReturnsAsync(projectToUpdate);

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}