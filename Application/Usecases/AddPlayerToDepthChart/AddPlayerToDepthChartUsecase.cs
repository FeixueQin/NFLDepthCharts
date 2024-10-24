using Application.Enums;
using Application.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Usecases.AddPlayerToDepthChart{
    public class AddPlayerToDepthChartUsecase : IAddPlayerToDepthChartUsecase
    {
        private readonly ITeamDepthChartRepo _teamDepthRepo;
        private readonly ILogger<AddPlayerToDepthChartUsecase> _logger;
        private IOutputPort? _outputPort;
        
        public AddPlayerToDepthChartUsecase(ITeamDepthChartRepo teamDepthRepo, ILogger<AddPlayerToDepthChartUsecase> logger){
            _teamDepthRepo = teamDepthRepo;
            _logger = logger;
        }
        
        public async Task Execute(PositionAbbre positionAbbre, int playerNumber, int? positionDepth = null)
        {
            if (positionDepth < 1)
            {
                _outputPort!.BadRequest("Position depth must be at least 1.");
                return;
            }
            try{
                var updatePositionDepth = positionDepth ?? 0;
                await _teamDepthRepo.InsertPlayerToDepthChart(positionAbbre, playerNumber, updatePositionDepth);
                _outputPort!.Success();
                
            }catch(Exception ex){
                _outputPort!.Failure(ex.Message);
                _logger.LogError(ex, "fail to add the data");
            }
        }

        public void SetOutputPort(IOutputPort outputPort)
        {
            _outputPort = outputPort;
        }
    }
}