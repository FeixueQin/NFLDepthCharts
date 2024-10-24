using Application.Usecases.GetBackups;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using Web.DTO;

namespace Web.UseCases.GetBackups{
    [ApiController]
    [Route("/get-backups")]
    public class GetBackupsController : ControllerBase
    {

        private readonly IGetBackupsUsecase _useCase;

        private readonly GetBackupsPresenter _presenter;

        public GetBackupsController(IGetBackupsUsecase usecase){
            _useCase = usecase;
            _presenter = new GetBackupsPresenter();
            _useCase.SetOutputPort(_presenter);
        }
        
        [HttpPost]
        [SwaggerRequestExample(typeof(GetBackupRequest), typeof(GetBackupRequestExample))]
        public async Task<IActionResult> AddPlayerToDepthChart([FromBody] GetBackupRequest request){
            await _useCase.Execute(request.PositionAbbre, request.PlayerNumber);
            return _presenter.ViewModel;
        }

    }
}