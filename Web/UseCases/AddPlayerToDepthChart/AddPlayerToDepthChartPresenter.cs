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

        public void BadRequest(List<string> errorMessages)
        {
            ViewModel = new BadRequestObjectResult(new { Errors = errorMessages });
        }

        public void Failure(string errorMessage)
        {
            ViewModel = new BadRequestObjectResult(new { Errors = errorMessage });
        }

        public void Success()
        {
            ViewModel = new OkResult();
        }
    }

}