using TransportEnterprise.Catalog.BLL.DTO;

namespace TransportEnterprise.Catalog.BLL.Services.Interfaces;

public interface IDispatcherService
{
    IEnumerable<DispatcherDTO> GetDispatchers();
}