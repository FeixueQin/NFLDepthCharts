
using Application.Usecases.AddPlayerToDepthChart;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Web.DTO;

namespace Web.UseCases.AddPlayerToDepthChart{
    [ApiController]
    [Route("[controller]")]
    public class AddPlayerToDepthChartController : ControllerBase
    {

        private readonly IAddPlayerToDepthChartUsecase _useCase;

        private readonly AddPlayerToDepthChartPresenter _presenter;

        public AddPlayerToDepthChartController(IAddPlayerToDepthChartUsecase usecase){
            _useCase = usecase;
            _presenter = new AddPlayerToDepthChartPresenter();
            _useCase.SetOutputPort(_presenter);
        }
        
        [HttpPost]
        [SwaggerRequestExample(typeof(AddPlayerToDepthChartRequest), typeof(AddPlayerToDepthChartRequestExample))]
        public async Task<IActionResult> AddPlayerToDepthChart([FromBody] AddPlayerToDepthChartRequest request){
            var asd = request;
            await _useCase.Execute(request.PositionAbbre, request.PlayerNumber, request.PositionDepth);
            return _presenter.ViewModel;
        }

    }
}