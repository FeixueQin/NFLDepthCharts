using Application.Models;
using Application.Usecases.RemovePlayerFromDepthChart;
using Microsoft.AspNetCore.Mvc;

namespace Web.UseCases.RemovePlayerFromDepthChart{
    public class RemovePlayerFromDepthChartPresenter : IOutputPort
    {
        public IActionResult? ViewModel { get; set; }

        public void BadRequest(string errorMessage)
        {
            ViewModel = new BadRequestObjectResult(new { Error = errorMessage });
        }

        public void Failure(string errorMessage)
        {
            ViewModel = new ObjectResult(new ProblemDetails
            {
                Title = "Failed To Remove Player From Team Depth Chart",
                Detail = errorMessage,
                Status = 500
            });
        }

        public void NotFound()
        {
            ViewModel = new OkObjectResult(new List<Player>());
        }

        public void Success(Player player)
        {
            ViewModel = new OkObjectResult(PlayerMapper.MapToResponseData(player));
        }
    }

}