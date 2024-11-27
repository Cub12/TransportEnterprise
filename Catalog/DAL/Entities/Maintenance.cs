namespace DAL.Entities;

public class Maintenance
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string? ServiceType { get; set; }
    public string? Description { get; set; }
    public IEnumerable<Vehicle>? Vehicles { get; set; }
}