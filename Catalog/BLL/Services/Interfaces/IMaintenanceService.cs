using TransportEnterprise.Catalog.BLL.DTO;

namespace TransportEnterprise.Catalog.BLL.Services.Interfaces;

public interface IMaintenanceService
{
    IEnumerable<MaintenanceDTO> GetMaintenances();
}