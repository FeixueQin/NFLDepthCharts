using Application.Enums;
using Swashbuckle.AspNetCore.Filters;

namespace Web.DTO{

    public class RemoveFromDepthChartRequestExample : IExamplesProvider<RemoveFromDepthChartRequest>
    {
        public RemoveFromDepthChartRequest GetExamples()
        {
            return new RemoveFromDepthChartRequest(){
                PlayerNumber = 2,
                PositionAbbre = PositionAbbre.C
            };
        }
    }
}