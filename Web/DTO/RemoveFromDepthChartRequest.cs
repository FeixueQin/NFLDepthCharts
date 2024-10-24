using Application.Enums;

namespace Web.DTO{
    public class RemoveFromDepthChartRequest{
        public int PlayerNumber { get; set; }

        public PositionAbbre PositionAbbre { get; set; }
    }
}