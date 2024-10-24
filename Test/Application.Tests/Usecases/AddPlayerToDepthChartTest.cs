using Application.Enums;
using Application.Repositories;
using Application.Usecases.AddPlayerToDepthChart;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Tests.Usecases
{
    public class AddPlayerToDepthChartUsecaseTests
    {
        private readonly Mock<ITeamDepthChartRepo> _mockRepo;
        private readonly Mock<ILogger<AddPlayerToDepthChartUsecase>> _mockLogger;
        private readonly Mock<IOutputPort> _mockOutputPort;
        private readonly AddPlayerToDepthChartUsecase _usecase;

        public AddPlayerToDepthChartUsecaseTests()
        {
            _mockRepo = new Mock<ITeamDepthChartRepo>();
            _mockLogger = new Mock<ILogger<AddPlayerToDepthChartUsecase>>();
            _mockOutputPort = new Mock<IOutputPort>();

            _usecase = new AddPlayerToDepthChartUsecase(_mockRepo.Object, _mockLogger.Object);
            _usecase.SetOutputPort(_mockOutputPort.Object);
        }

        [Fact]
        public async Task Execute_ShouldCallSuccess_WhenPlayerIsAdded()
        {
            // Arrange
            var positionAbbre = PositionAbbre.LWR;
            var playerNumber = 12;
            var positionDepth = 1;

            _mockRepo.Setup(repo => repo.InsertPlayerToDepthChart(positionAbbre, playerNumber, positionDepth))
                      .Returns(Task.CompletedTask);

            // Act
            await _usecase.Execute(positionAbbre, playerNumber, positionDepth);

            // Assert
            _mockOutputPort.Verify(output => output.Success(), Times.Once);
            _mockOutputPort.Verify(output => output.Failure(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task Execute_ShouldCallFailure_WhenExceptionOccurs()
        {
            // Arrange
            var positionAbbre = PositionAbbre.LWR;
            var playerNumber = 12;
            var positionDepth = 1;

            var exceptionMessage = "Database error";
            _mockRepo.Setup(repo => repo.InsertPlayerToDepthChart(positionAbbre, playerNumber, positionDepth))
                      .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            await _usecase.Execute(positionAbbre, playerNumber, positionDepth);

            // Assert
            _mockOutputPort.Verify(output => output.Failure(exceptionMessage), Times.Once);
            _mockOutputPort.Verify(output => output.Success(), Times.Never);
        }
    }
}