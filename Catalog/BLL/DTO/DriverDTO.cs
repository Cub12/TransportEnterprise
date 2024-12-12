namespace TransportEnterprise.Catalog.BLL.DTO;

public class DriverDTO
{
    public int DriverId { get; set; }
    public string FullName { get; set; }
    public string CardNumber { get; set; }
    public DateTime? BirthDate { get; set; }
    public int TransportEnterpriseId { get; set; }
}