using AutoMapper;
using TransportEnterprise.Catalog.BLL.DTO;
using TransportEnterprise.Catalog.BLL.Services.Interfaces;
using TransportEnterprise.Catalog.CCL.Security;
using TransportEnterprise.Catalog.CCL.Security.Identity;
using TransportEnterprise.Catalog.DAL.UnitOfWork;
using Dispatcher = TransportEnterprise.Catalog.DAL.Entities.Dispatcher;
using Driver = TransportEnterprise.Catalog.DAL.Entities.Driver;

namespace TransportEnterprise.Catalog.BLL.Services.Impl;

public class DriverService : IDriverService
{
    private readonly IUnitOfWork _database;

    public DriverService(IUnitOfWork unitOfWork)
    {
        if (unitOfWork == null)
        {
            throw new ArgumentNullException(nameof(unitOfWork));
        }

        _database = unitOfWork;
    }

    public IEnumerable<DriverDTO> GetDrivers()
    {
        var user = SecurityContext.GetUser();
        var userType = user.GetType();
        if (userType != typeof(Admin) && userType != typeof(Dispatcher) && userType != typeof(Driver))
        {
            throw new MethodAccessException();
        }
        var transportEnterpriseId = user.TransportEnterpriseId;
        var driversEntities = _database.Drivers
            .Find(d => d.TransportEnterpriseId == transportEnterpriseId);
        var mapper = new MapperConfiguration(cfg => cfg
            .CreateMap<Driver, DriverDTO>()).CreateMapper();
        var driversDTO = mapper
            .Map<IEnumerable<Driver>, List<DriverDTO>>(driversEntities);
        return driversDTO;
    }
}