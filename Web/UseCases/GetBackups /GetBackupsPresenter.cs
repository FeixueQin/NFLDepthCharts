using Application.Models;
using Application.Usecases.GetBackups;
using Microsoft.AspNetCore.Mvc;

namespace Web.UseCases.GetBackups{
    public class GetBackupsPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; set; } = new OkObjectResult(new List<Player>());

        public void BadRequest(string errorMessage)
        {
            ViewModel = new BadRequestObjectResult(new { Errors = errorMessage });
        }

        public void Failure(string errorMessage)
        {
            ViewModel = new ObjectResult(new ProblemDetails
            {
                Title = "Failed To Get Backup Players From Team Depth Chart",
                Detail = errorMessage,
                Status = 500
            });
        }

        public void Success(List<Player> players)
        {
            var playersResponse = players.Select(s => PlayerMapper.MapToResponseData(s));
            ViewModel = new OkObjectResult(playersResponse);
        }
    }

}