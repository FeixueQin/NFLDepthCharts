using Application.Models;
using Application.Usecases.GetBackups;
using Microsoft.AspNetCore.Mvc;

namespace Web.UseCases.GetBackups{
    public class GetBackupsPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; set; } 

        public void BadRequest(string errorMessage)
        {
            ViewModel = new BadRequestObjectResult(new { Errors = errorMessage });
        }

        public void Failure(string errorMessage)
        {
            ViewModel = new BadRequestObjectResult(new { Errors = errorMessage });
        }

        public void Success(List<Player> players)
        {
            var playersResponse = players.Select(s => PlayerMapper.MapToResponseData(s));
            ViewModel = new OkObjectResult(playersResponse);
        }
    }

}