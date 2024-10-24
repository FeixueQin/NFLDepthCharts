using Application.Models;
using Application.Usecases.GetFullDepthChart;
using Microsoft.AspNetCore.Mvc;

namespace Web.UseCases.GetFullDepthChart{
    public class GetFullDepthChartPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; set; } 

        public void Failure(string errorMessage)
        {
            ViewModel = new BadRequestObjectResult(new { Errors = errorMessage });
        }

        public void Success(List<Depth> fullDepth)
        {
            var fullDepthDataResponse = FullDepthChartMapper.MapDepthChart(fullDepth);
            ViewModel = new OkObjectResult(fullDepthDataResponse);
        }
    }

}