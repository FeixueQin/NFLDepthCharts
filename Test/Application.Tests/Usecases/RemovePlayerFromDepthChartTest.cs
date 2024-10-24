using Application.Enums;
using Application.Models;
using Application.Repositories;
using Application.Usecases.RemovePlayerFromDepthChart;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Tests.Usecases
{
    public class RemovePlayerToDepthChartUsecaseTests
    {
        private readonly Mock<ITeamDepthChartRepo> _mockTeamDepthRepo;
        private readonly Mock<IPlayerRepo> _mockPlayerRepo;
        private readonly Mock<ILogger<RemovePlayerToDepthChartUsecase>> _mockLogger;
        private readonly RemovePlayerToDepthChartUsecase _usecase;
        private readonly Mock<IOutputPort> _mockOutputPort;

        public RemovePlayerToDepthChartUsecaseTests()
        {
            _mockTeamDepthRepo = new Mock<ITeamDepthChartRepo>();
            _mockPlayerRepo = new Mock<IPlayerRepo>();
            _mockLogger = new Mock<ILogger<RemovePlayerToDepthChartUsecase>>();
            _mockOutputPort = new Mock<IOutputPort>();
            _usecase = new RemovePlayerToDepthChartUsecase(
                _mockTeamDepthRepo.Object,
                _mockPlayerRepo.Object,
                _mockLogger.Object);
            _usecase.SetOutputPort(_mockOutputPort.Object);
        }

        [Fact]
        public async Task Execute_ShouldRemovePlayer_WhenPlayerExists()
        {
            // Arrange
            var positionAbbre = PositionAbbre.LWR;
            var playerNumber = 12;

            _mockTeamDepthRepo.Setup(repo => repo.GetPlayerFromDepthChart(positionAbbre, playerNumber))
                .ReturnsAsync(new Depth { PlayerNumber = playerNumber });

            _mockTeamDepthRepo.Setup(repo => repo.RemovePlayerFromDepthChart(positionAbbre, playerNumber))
                .Returns(Task.CompletedTask);

            _mockPlayerRepo.Setup(repo => repo.GetPlayer(playerNumber))
                .ReturnsAsync(new Player { PlayerNumber = playerNumber, Name = "TestName", PositionId = 1 });

            // Act
            await _usecase.Execute(positionAbbre, playerNumber);

            // Assert
            _mockOutputPort.Verify(output => output.Success(It.IsAny<Player>()), Times.Once);
            _mockOutputPort.Verify(output => output.NotFound(), Times.Never);
        }

        [Fact]
        public async Task Execute_ShouldNotFound_WhenPlayerDoesNotExist()
        {
            // Arrange
            var positionAbbre = PositionAbbre.LWR;
            var playerNumber = 12;

            _mockTeamDepthRepo.Setup(repo => repo.GetPlayerFromDepthChart(positionAbbre, playerNumber))
                .ReturnsAsync((Depth)null); // Simulate player not found

            // Act
            await _usecase.Execute(positionAbbre, playerNumber);

            // Assert
            _mockOutputPort.Verify(output => output.NotFound(), Times.Once);
            _mockOutputPort.Verify(output => output.Success(It.IsAny<Player>()), Times.Never);
        }

        [Fact]
        public async Task Execute_ShouldLogError_WhenExceptionOccurs()
        {
            // Arrange
            var positionAbbre = PositionAbbre.LWR;
            var playerNumber = 12;

            _mockTeamDepthRepo.Setup(repo => repo.GetPlayerFromDepthChart(positionAbbre, playerNumber))
                .ThrowsAsync(new Exception("Database error")); // Simulate an exception

            // Act
            await _usecase.Execute(positionAbbre, playerNumber);

            // Assert
            _mockOutputPort.Verify(output => output.Failure(It.IsAny<string>()), Times.Once);
        }
    }
}