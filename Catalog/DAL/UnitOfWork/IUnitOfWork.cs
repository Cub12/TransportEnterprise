using DAL.Repositories.Interfaces;

namespace DAL.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IDispatcherRepository Dispatchers { get; }
    IDriverRepository Drivers { get; }
    IMaintenanceRepository Maintenances { get; }
    IRouteRepository Routes { get; }
    IVehicleRepository Vehicles { get; }
    void Save();
}