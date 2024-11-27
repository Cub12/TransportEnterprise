namespace DAL.Entities;

public class Route
{
    public int Id { get; set; }
    public string? StartingPoint { get; set; }
    public string? EndPoint { get; set; }
    public float Length { get; set; }
    public IEnumerable<Driver>? Drivers { get; set; }
    public IEnumerable<Vehicle>? Vehicles { get; set; }
}