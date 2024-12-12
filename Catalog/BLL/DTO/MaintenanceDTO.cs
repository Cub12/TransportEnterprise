namespace TransportEnterprise.Catalog.BLL.DTO;

public class MaintenanceDTO
{
    public int MaintenanceId { get; set; }
    public DateTime Date { get; set; }
    public string ServiceType { get; set; }
    public string? Description { get; set; }
    public int TransportEnterpriseId { get; set; }
}