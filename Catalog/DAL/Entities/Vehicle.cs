namespace DAL.Entities;

public class Vehicle
{
    public int Id { get; set; }
    public string? LicensePlate { get; set; }
    public string? Model { get; set; }
    public int ProductionYear { get; set; }
    public string? Type { get; set; }
}