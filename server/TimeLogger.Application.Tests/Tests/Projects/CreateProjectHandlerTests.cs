using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TimeLogger.Application.Common.Exceptions;
using TimeLogger.Application.Features.Projects.Commands.CreateProject;
using TimeLogger.Domain.Entities;
using TimeLogger.Domain.Repositories;
using TimeLogger.Domain.Repositories.Common;
using Xunit;

namespace TimeLogger.Application.Tests.Tests.Projects
{
    public class CreateProjectHandlerTests
    {
        [Fact]
        public async Task Handle_CreateProject_Success()
        {
            // Arrange
            var iUnitOfWorkMock = new Mock<IUnitOfWork>();
            var projectRepositoryMock = new Mock<IProjectRepository>();
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var mapperMock = new Mock<IMapper>();

            var createProjectCommand = new CreateProjectCommand
            {
                CustomerId = 1,
                Name = "foo"
            };

            // Configure customerRepositoryMock to return a valid customer
            customerRepositoryMock
                .Setup(repo => repo.GetSingle(createProjectCommand.CustomerId, It.IsAny<CancellationToken>(), default))
                .ReturnsAsync(new Customer());

            var handler = new CreateProjectHandler(
                iUnitOfWorkMock.Object,
                projectRepositoryMock.Object,
                customerRepositoryMock.Object,
                mapperMock.Object);

            // Act
            await handler.Handle(createProjectCommand, CancellationToken.None);

            // Assert

            // Verify that customerRepository.GetSingle is called with the correct customer ID
            customerRepositoryMock.Verify(
                repo => repo.GetSingle(createProjectCommand.CustomerId, It.IsAny<CancellationToken>(), default),
                Times.Once);

            // Verify that projectRepository.Create is called with the mapped project
            projectRepositoryMock.Verify(
                repo => repo.Create(It.IsAny<Project>()),
                Times.Once);
        }

        [Fact]
        public async Task Handle_InvalidCustomerWhileCreatingProject_ThrowsBadRequestException()
        {
            // Arrange
            var iUnitOfWorkMock = new Mock<IUnitOfWork>();
            var projectRepositoryMock = new Mock<IProjectRepository>();
            var customerRepositoryMock = new Mock<ICustomerRepository>();
            var mapperMock = new Mock<IMapper>();

            var createProjectCommand = new CreateProjectCommand
            {
                CustomerId = 1,
            };

            // Configure customerRepositoryMock to return null, indicating an invalid customer
            customerRepositoryMock
                .Setup(repo => repo.GetSingle(createProjectCommand.CustomerId, It.IsAny<CancellationToken>(), false))
                .ReturnsAsync((Customer)null);

            var handler = new CreateProjectHandler(
                iUnitOfWorkMock.Object,
                projectRepositoryMock.Object,
                customerRepositoryMock.Object,
                mapperMock.Object);

            // Act and Assert
            await Assert.ThrowsAsync<BadRequestException>(() =>
                handler.Handle(createProjectCommand, CancellationToken.None));
        }
    }
}