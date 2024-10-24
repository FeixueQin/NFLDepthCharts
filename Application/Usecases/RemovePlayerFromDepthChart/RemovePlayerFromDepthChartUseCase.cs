using Application.Enums;
using Application.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Usecases.RemovePlayerFromDepthChart{
    public class RemovePlayerToDepthChartUsecase : IRemovePlayerFromDepthChartUsecase
    {
        private readonly ITeamDepthChartRepo _teamDepthRepo;
        private readonly IPlayerRepo _playerRepo;
        private readonly ILogger<RemovePlayerToDepthChartUsecase> _logger;
        private IOutputPort? _outputPort;
        
        public RemovePlayerToDepthChartUsecase(ITeamDepthChartRepo teamDepthRepo, IPlayerRepo playerRepo, ILogger<RemovePlayerToDepthChartUsecase> logger){
            _teamDepthRepo = teamDepthRepo;
            _playerRepo = playerRepo;
            _logger = logger;
        }
        
        public async Task Execute(PositionAbbre positionAbbre, int playerNumber)
        {
            try{
                var existingPlayerInDepth = await _teamDepthRepo.GetPlayerFromDepthChart(positionAbbre, playerNumber);
                if(existingPlayerInDepth == null){
                    _outputPort!.NotFound();
                }else{
                    await _teamDepthRepo.RemovePlayerFromDepthChart(positionAbbre, playerNumber);
                    var player = await _playerRepo.GetPlayer(playerNumber);
                    _outputPort!.Success(player);
                }
                
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