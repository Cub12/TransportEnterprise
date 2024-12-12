namespace TransportEnterprise.Catalog.BLL.DTO;

public class VehicleDTO
{
    public int VehicleId { get; set; }
    public string LicensePlate { get; set; }
    public string Model { get; set; }
    public int ProductionYear { get; set; }
    public string Type { get; set; }
    public int TransportEnterpriseId { get; set; }
}