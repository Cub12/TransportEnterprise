using AutoMapper;
using TransportEnterprise.Catalog.BLL.DTO;
using TransportEnterprise.Catalog.BLL.Services.Interfaces;
using TransportEnterprise.Catalog.CCL.Security;
using TransportEnterprise.Catalog.CCL.Security.Identity;
using TransportEnterprise.Catalog.DAL.Entities;
using TransportEnterprise.Catalog.DAL.UnitOfWork;
using Dispatcher = TransportEnterprise.Catalog.CCL.Security.Identity.Dispatcher;
using Driver = TransportEnterprise.Catalog.DAL.Entities.Driver;

namespace TransportEnterprise.Catalog.BLL.Services.Impl;

public class RouteService : IRouteService
{
    private readonly IUnitOfWork _database;

    public RouteService(IUnitOfWork unitOfWork)
    {
        if (unitOfWork == null)
        {
            throw new ArgumentNullException(nameof(unitOfWork));
        }

        _database = unitOfWork;
    }

    public IEnumerable<RouteDTO> GetRoutes()
    {
        var user = SecurityContext.GetUser();
        var userType = user.GetType();
        if (userType != typeof(Admin) && userType != typeof(Dispatcher) && userType != typeof(Driver))
        {
            throw new MethodAccessException();
        }
        var transportEnterpriseId = user.TransportEnterpriseId;
        var routesEntities = _database.Routes
            .Find(r => r.TransportEnterpriseId == transportEnterpriseId);
        var mapper = new MapperConfiguration(cfg => cfg
            .CreateMap<Route, RouteDTO>()).CreateMapper();
        var routesDTO = mapper
            .Map<IEnumerable<Route>, List<RouteDTO>>(routesEntities);
        return routesDTO;
    }
}