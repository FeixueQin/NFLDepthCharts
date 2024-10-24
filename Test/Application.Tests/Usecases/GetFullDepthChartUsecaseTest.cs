using Application.Enums;
using Application.Models;
using Application.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using Application.Usecases.GetFullDepthChart;

namespace Application.Test.Usecases
{
    public class GetFullDepthChartUsecaseTests
    {
        private readonly Mock<ITeamDepthChartRepo> _teamDepthRepoMock;
        private readonly Mock<IOutputPort> _outputPortMock;
        private readonly Mock<ILogger<GetFullDepthChartUsecase>> _loggerMock;
        private readonly GetFullDepthChartUsecase _usecase;

        public GetFullDepthChartUsecaseTests()
        {
            _teamDepthRepoMock = new Mock<ITeamDepthChartRepo>();
            _outputPortMock = new Mock<IOutputPort>();
            _loggerMock = new Mock<ILogger<GetFullDepthChartUsecase>>();
            _usecase = new GetFullDepthChartUsecase(_teamDepthRepoMock.Object, _loggerMock.Object);
            _usecase.SetOutputPort(_outputPortMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldReturnFullDepthCharts_WhenSuccessful()
        {
            // Arrange
            var depthCharts = new List<Depth>
            {
                new Depth { PlayerNumber = 10, PositionDepth = 1, PositionId = (int)PositionAbbre.LWR },
                new Depth { PlayerNumber = 11, PositionDepth = 2, PositionId = (int)PositionAbbre.RWR }
            };

            _teamDepthRepoMock
                .Setup(repo => repo.GetAllPosisionDepth())
                .ReturnsAsync(depthCharts);

            // Act
            await _usecase.Execute();

            // Assert
            _outputPortMock.Verify(port => port.Success(depthCharts.ToList()), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldCallFailure_WhenExceptionOccurs()
        {
            // Arrange
            var exceptionMessage = "An error occurred";
            _teamDepthRepoMock
                .Setup(repo => repo.GetAllPosisionDepth())
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            await _usecase.Execute();

            // Assert
            _outputPortMock.Verify(port => port.Failure(exceptionMessage), Times.Once);
        }
    }
}