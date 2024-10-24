using Application.Enums;
using Application.Models;
using Application.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Usecases.GetBackups{
    public class GetBackupsUsecase : IGetBackupsUsecase
    {
        private readonly ITeamDepthChartRepo _teamDepthRepo;
        private readonly IPlayerRepo _playerRepo;
        private readonly ILogger<GetBackupsUsecase> _logger;
        private IOutputPort? _outputPort;
        
        public GetBackupsUsecase(ITeamDepthChartRepo teamDepthRepo, IPlayerRepo playerRepo, ILogger<GetBackupsUsecase> logger){
            _teamDepthRepo = teamDepthRepo;
            _playerRepo = playerRepo;
            _logger = logger;
        }
        
        public async Task Execute(PositionAbbre positionAbbre, int playerNumber)
        {
            try{
                var backups = await _teamDepthRepo.GetBackups(positionAbbre, playerNumber);
                var playerArray = await Task.WhenAll(
                    backups.Select(async backup => await _playerRepo.GetPlayer(backup.PlayerNumber))
                );

                _outputPort!.Success(playerArray.ToList());

            }catch(Exception ex){
                _outputPort!.Failure(ex.Message);
                _logger.LogError(ex, "Failed to remove the player from Depth Charts");
            }
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}