using Application.Models;
using Application.Usecases.GetFullDepthChart;
using Microsoft.AspNetCore.Mvc;

namespace Web.UseCases.GetFullDepthChart{
    public class GetFullDepthChartPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; set; } 

        public void Failure(string errorMessage)
        {
            ViewModel = new ObjectResult(new ProblemDetails
            {
                Title = "Failed To Get Team Depth Chart",
                Detail = errorMessage,
                Status = 500
            });
        }

        public void Success(List<Depth> fullDepth)
        {
            var fullDepthDataResponse = FullDepthChartMapper.MapDepthChart(fullDepth);
            ViewModel = new OkObjectResult(fullDepthDataResponse);
        }
    }

}