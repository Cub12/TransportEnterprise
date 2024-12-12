namespace TransportEnterprise.Catalog.BLL.DTO;

public class RouteDTO
{
    public int RouteId { get; set; }
    public string StartingPoint { get; set; }
    public string EndPoint { get; set; }
    public float Length { get; set; }
    public int TransportEnterpriseId { get; set; }
}