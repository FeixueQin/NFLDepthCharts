using Application.Usecases.GetFullDepthChart;
using Microsoft.AspNetCore.Mvc;
using Web.DTO;

namespace Web.UseCases.GetFullDepthChart{
    [ApiController]
    [Route("/get-full-depth")]
    public class GetFullDepthController : ControllerBase
    {

        private readonly IGetFullDepthChartUsecase _useCase;

        private readonly GetFullDepthChartPresenter _presenter;

        public GetFullDepthController(IGetFullDepthChartUsecase usecase){
            _useCase = usecase;
            _presenter = new GetFullDepthChartPresenter();
            _useCase.SetOutputPort(_presenter);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetFullDepthChart(){
            await _useCase.Execute();
            return _presenter.ViewModel;
        }

    }
}