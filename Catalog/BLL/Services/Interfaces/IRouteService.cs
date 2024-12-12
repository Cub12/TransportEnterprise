using TransportEnterprise.Catalog.BLL.DTO;

namespace TransportEnterprise.Catalog.BLL.Services.Interfaces;

public interface IRouteService
{
    IEnumerable<RouteDTO> GetRoutes();
}