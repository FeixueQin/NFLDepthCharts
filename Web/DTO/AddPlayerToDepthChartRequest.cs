using Application.Enums;

namespace Web.DTO{
    public class AddPlayerToDepthChartRequest{
        public int PlayerNumber { get; set; }

        public int? PositionDepth { get; set; }

        public PositionAbbre PositionAbbre { get; set; }
    }
}