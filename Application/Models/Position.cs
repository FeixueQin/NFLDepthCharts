namespace Application.Models{
    public class Position
{
    public int PositionId { get; set; } // Primary Key

    public string Name { get; set; }

    public string Abbreviation { get; set; }

    public ICollection<Depth> Depths { get; set; }
}

}