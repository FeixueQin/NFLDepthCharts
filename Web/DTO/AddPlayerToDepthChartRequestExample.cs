using Application.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Web.DTO{

    public class AddPlayerToDepthChartRequestExample : IExamplesProvider<AddPlayerToDepthChartRequest>
    {
        public AddPlayerToDepthChartRequest GetExamples()
        {
            return new AddPlayerToDepthChartRequest(){
                PlayerNumber = 2,
                PositionDepth = 1,
                PositionAbbre = PositionAbbre.C
            };
        }
    }
}