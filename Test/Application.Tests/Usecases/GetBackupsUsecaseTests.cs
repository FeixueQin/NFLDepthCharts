using Application.Enums;
using Application.Models;
using Application.Repositories;
using Application.Usecases.GetBackups;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Test.Usecases
{
    public class GetBackupsUsecaseTests
    {
        private readonly Mock<ITeamDepthChartRepo> _teamDepthRepoMock;
        private readonly Mock<IPlayerRepo> _playerRepoMock;
        private readonly Mock<ILogger<GetBackupsUsecase>> _loggerMock;
        private readonly Mock<IOutputPort> _outputPortMock;
        private readonly GetBackupsUsecase _usecase;

        public GetBackupsUsecaseTests()
        {
            _teamDepthRepoMock = new Mock<ITeamDepthChartRepo>();
            _playerRepoMock = new Mock<IPlayerRepo>();
            _loggerMock = new Mock<ILogger<GetBackupsUsecase>>();
            _outputPortMock = new Mock<IOutputPort>();
            _usecase = new GetBackupsUsecase(_teamDepthRepoMock.Object, _playerRepoMock.Object, _loggerMock.Object);
            _usecase.SetOutputPort(_outputPortMock.Object);
        }

        [Fact]
        public async Task Execute_ShouldCallSuccess_WhenBackupsAreFound()
        {
            // Arrange
            var positionAbbre = PositionAbbre.LWR;
            var playerNumber = 21;
            var backups = new List<Depth>
            {
                new Depth { PlayerNumber = 22 },
                new Depth { PlayerNumber = 23 }
            };
            var players = new List<Player>
            {
                new Player { PlayerNumber = 22, Name = "Player 22", PositionId = (int)positionAbbre },
                new Player { PlayerNumber = 23, Name = "Player 23", PositionId = (int)positionAbbre }
            };

            _teamDepthRepoMock.Setup(repo => repo.GetBackups(positionAbbre, playerNumber))
                .ReturnsAsync(backups);
            _playerRepoMock.Setup(repo => repo.GetPlayer(22))
                .ReturnsAsync(players[0]);
            _playerRepoMock.Setup(repo => repo.GetPlayer(23))
                .ReturnsAsync(players[1]);

            // Act
            await _usecase.Execute(positionAbbre, playerNumber);

            // Assert
            _outputPortMock.Verify(port => port.Success(It.Is<List<Player>>(list => 
                list.Count == 2 &&
                list[0].PlayerNumber == 22 &&
                list[1].PlayerNumber == 23)), Times.Once);
        }

        [Fact]
        public async Task Execute_ShouldCallFailure_WhenExceptionOccurs()
        {
            // Arrange
            var positionAbbre = PositionAbbre.LWR;
            var playerNumber = 21;
            var exceptionMessage = "An error occurred";

            var ex = _teamDepthRepoMock.Setup(repo => repo.GetBackups(positionAbbre, playerNumber))
                .ThrowsAsync(new Exception(exceptionMessage));

            // Act
            await _usecase.Execute(positionAbbre, playerNumber);

            // Assert
            _outputPortMock.Verify(port => port.Failure(exceptionMessage), Times.Once);
        }
    }
}
