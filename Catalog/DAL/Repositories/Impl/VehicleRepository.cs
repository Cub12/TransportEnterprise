using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Impl;

public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
{
    internal VehicleRepository(TransportEnterpriseContext context) : base(context)
    {
    }
    
    public IEnumerable<Vehicle> GetVehiclesByType(string type)
    {
        return Find(v => v.Type != null && v.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
    }
    
    public IEnumerable<Vehicle> GetVehiclesByModel(string model)
    {
        return Find(v => v.Model != null && v.Model.Equals(model, StringComparison.OrdinalIgnoreCase));
    }
}