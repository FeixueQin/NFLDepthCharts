using Application.Enums;

namespace Web.DTO{
    public class PlayerResponseData{
        public int PlayerNumber { get; set; }

        public required string PositionAbbre { get; set; }

        public required string Name { get; set; }
    }

}