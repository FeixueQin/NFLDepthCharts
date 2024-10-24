using Application.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.Usecases.GetFullDepthChart{
    public class GetFullDepthChartUsecase : IGetFullDepthChartUsecase
    {
        private readonly ITeamDepthChartRepo _teamDepthRepo;
        private readonly ILogger<GetFullDepthChartUsecase> _logger;
        private IOutputPort? _outputPort;
        
        public GetFullDepthChartUsecase(ITeamDepthChartRepo teamDepthRepo, ILogger<GetFullDepthChartUsecase> logger){
            _teamDepthRepo = teamDepthRepo;
            _logger = logger;
        }
        
        public async Task Execute()
        {
            try{
                var fullDepthCharts = await _teamDepthRepo.GetAllPosisionDepth();
                _outputPort!.Success(fullDepthCharts.ToList());

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