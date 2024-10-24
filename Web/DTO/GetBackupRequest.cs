using Application.Enums;

namespace Web.DTO{
    public class GetBackupRequest{
        public int PlayerNumber { get; set; }

        public PositionAbbre PositionAbbre { get; set; }
    }
}