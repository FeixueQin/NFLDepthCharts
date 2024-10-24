using Application.Usecases.AddPlayerToDepthChart;
using Microsoft.AspNetCore.Mvc;

namespace Web.UseCases.AddPlayerToDepthChart{
    public class AddPlayerToDepthChartPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; set; } 

        public void BadRequest(string errorMessage)
        {
            ViewModel = new BadRequestObjectResult(new { Error = errorMessage });
        }

        public void Failure(string errorMessage)
        {
            ViewModel = new ObjectResult(new ProblemDetails
            {
                Title = "Failed To Add Player To Team Depth Chart",
                Detail = errorMessage,
                Status = 500
            });
        }

        public void Success()
        {
            ViewModel = new OkResult();
        }
    }

}