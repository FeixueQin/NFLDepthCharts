namespace Application.Models{
    public class Position
{
    public int PositionId { get; set; } // Primary Key

    public string Name { get; set; } // Position name, e.g., "LWR", "RWR", etc.

    public string Abbreviation { get; set; } // Abbreviation for the position

    // Navigation property for DepthChart (if needed)
    public ICollection<Depth> Depths { get; set; }
}

}