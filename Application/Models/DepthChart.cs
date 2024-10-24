namespace Application.Models{
    public class Depth{
        public int DepthChartId { get; set; } // Primary Key

        public int PlayerNumber { get; set; } // Foreign Key from Players
        public int PositionId { get; set; } // Foreign Key from Positions
        public int PositionDepth { get; set; }
        public Player Player { get; set; }
        public Position Position { get; set; }
    }
}