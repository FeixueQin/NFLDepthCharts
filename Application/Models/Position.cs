namespace Application.Models{
    public class Position
    {
        public int PositionId { get; set; } // Primary Key

        public required string Name { get; set; }

        public required string Abbreviation { get; set; }

        public ICollection<Depth> Depths { get; set; }
    }
}