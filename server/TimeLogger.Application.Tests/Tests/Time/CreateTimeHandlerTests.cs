using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TimeLogger.Application.Common.Exceptions;
using TimeLogger.Application.Features.Times.Commands.Create;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;
using Xunit;

namespace TimeLogger.Application.Tests.Tests.Time
{
    public class CreateTimeHandlerTests
    {
        [Fact]
        public async Task Handle_WhenProjectIsCompleted_ThrowsBadRequestException()
        {
            // Arrange
            var iUnitOfWorkMock = new Mock<IUnitOfWork>();
            var projectId = 1;
            var completedProject = new Project { CompletedAt = DateTime.Now }; // Set CompletedAt to a non-null value
            var createCommand = new CreateTimeCommand { ProjectId = projectId };
            
            var projectRepositoryMock = new Mock<IProjectRepository>();
            projectRepositoryMock
                .Setup(repo => repo.GetSingle(It.IsAny<int>(), It.IsAny<CancellationToken>(), It.IsAny<bool>()))
                .ReturnsAsync(completedProject);

            var timeRepositoryMock = new Mock<ITimeRepository>();
            var mapperMock = new Mock<IMapper>();

            var handler = new CreateTimeHandler(
                iUnitOfWorkMock.Object,
                timeRepositoryMock.Object,
                projectRepositoryMock.Object,
                mapperMock.Object
            );

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(async () =>
            {
                await handler.Handle(createCommand, CancellationToken.None);
            });

            // Ensure that the GetSingle method was called with the correct projectId
            projectRepositoryMock.Verify(repo => repo.GetSingle(projectId, It.IsAny<CancellationToken>(), true), Times.Once);
        }
    }
}