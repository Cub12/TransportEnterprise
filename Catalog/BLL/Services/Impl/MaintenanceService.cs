using AutoMapper;
using TransportEnterprise.Catalog.BLL.DTO;
using TransportEnterprise.Catalog.BLL.Services.Interfaces;
using TransportEnterprise.Catalog.CCL.Security;
using TransportEnterprise.Catalog.CCL.Security.Identity;
using TransportEnterprise.Catalog.DAL.Entities;
using TransportEnterprise.Catalog.DAL.UnitOfWork;

namespace TransportEnterprise.Catalog.BLL.Services.Impl;

public class MaintenanceService : IMaintenanceService
{
    private readonly IUnitOfWork _database;

    public MaintenanceService(IUnitOfWork unitOfWork)
    {
        if (unitOfWork == null)
        {
            throw new ArgumentNullException(nameof(unitOfWork));
        }

        _database = unitOfWork;
    }

    public IEnumerable<MaintenanceDTO> GetMaintenances()
    {
        var user = SecurityContext.GetUser();
        var userType = user.GetType();
        if (userType != typeof(Admin) && userType != typeof(Mechanic))
        {
            throw new MethodAccessException();
        }
        var transportEnterpriseId = user.TransportEnterpriseId;
        var maintenancesEntities = _database.Maintenances
            .Find(m => m.TransportEnterpriseId == transportEnterpriseId);
        var mapper = new MapperConfiguration(cfg => cfg
            .CreateMap<Maintenance, MaintenanceDTO>()).CreateMapper();
        var maintenancesDTO = mapper
            .Map<IEnumerable<Maintenance>, List<MaintenanceDTO>>(maintenancesEntities);
        return maintenancesDTO;
    }
}