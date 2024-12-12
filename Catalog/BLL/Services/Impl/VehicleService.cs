using AutoMapper;
using TransportEnterprise.Catalog.BLL.DTO;
using TransportEnterprise.Catalog.BLL.Services.Interfaces;
using TransportEnterprise.Catalog.CCL.Security;
using TransportEnterprise.Catalog.DAL.Entities;
using TransportEnterprise.Catalog.DAL.UnitOfWork;

namespace TransportEnterprise.Catalog.BLL.Services.Impl;

public class VehicleService : IVehicleService
{
    private readonly IUnitOfWork _database;

    public VehicleService(IUnitOfWork unitOfWork)
    {
        if (unitOfWork == null)
        {
            throw new ArgumentNullException(nameof(unitOfWork));
        }

        _database = unitOfWork;
    }

    public IEnumerable<VehicleDTO> GetVehicles()
    {
        var user = SecurityContext.GetUser();
        var transportEnterpriseId = user.TransportEnterpriseId;
        var vehiclesEntities = _database.Vehicles
            .Find(v => v.TransportEnterpriseId == transportEnterpriseId);
        var mapper = new MapperConfiguration(cfg => cfg
            .CreateMap<Vehicle, VehicleDTO>()).CreateMapper();
        var vehiclesDTO = mapper.Map<IEnumerable<Vehicle>, List<VehicleDTO>>(vehiclesEntities);
        return vehiclesDTO;
    }
}