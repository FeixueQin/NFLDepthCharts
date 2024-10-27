using Application.Usecases.RemovePlayerFromDepthChart;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Web.DTO;

namespace Web.UseCases.RemovePlayerFromDepthChart{
    [ApiController]
    [Route("/team-depth")]
    public class RemovePlayerFromDepthChartController : ControllerBase
    {
        private readonly IRemovePlayerFromDepthChartUsecase _useCase;

        private readonly RemovePlayerFromDepthChartPresenter _presenter;

        public RemovePlayerFromDepthChartController(IRemovePlayerFromDepthChartUsecase usecase){
            _useCase = usecase;
            _presenter = new RemovePlayerFromDepthChartPresenter();
            _useCase.SetOutputPort(_presenter);
        }
        
        [HttpDelete]
        [SwaggerRequestExample(typeof(RemoveFromDepthChartRequest), typeof(RemoveFromDepthChartRequestExample))]
        public async Task<IActionResult> RemovePlayerToDepthChart([FromBody] RemoveFromDepthChartRequest request){
            await _useCase.Execute(request.PositionAbbre, request.PlayerNumber);
            return _presenter.ViewModel;
        }
    }
}