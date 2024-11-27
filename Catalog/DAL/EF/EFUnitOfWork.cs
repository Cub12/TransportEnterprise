using DAL.Repositories.Impl;
using DAL.Repositories.Interfaces;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class EFUnitOfWork : IUnitOfWork
{
    private TransportEnterpriseContext _db;
    private DispatcherRepository _dispatcherRepository;
    private DriverRepository _driverRepository;
    private MaintenanceRepository _maintenanceRepository;
    private RouteRepository _routeRepository;
    private VehicleRepository _vehicleRepository;
    
    public EFUnitOfWork(DbContextOptions options)
    {
        _db = new TransportEnterpriseContext(options);
    }
    
    public IDispatcherRepository Dispatchers
    {
        get
        {
            if (_dispatcherRepository == null)
                _dispatcherRepository = new DispatcherRepository(_db);
            return _dispatcherRepository;
        }
    }
    
    public IDriverRepository Drivers
    {
        get
        {
            if (_driverRepository == null)
                _driverRepository = new DriverRepository(_db);
            return _driverRepository;
        }
    }
    
    public IMaintenanceRepository Maintenances
    {
        get
        {
            if (_maintenanceRepository == null)
                _maintenanceRepository = new MaintenanceRepository(_db);
            return _maintenanceRepository;
        }
    }
    
    public IRouteRepository Routes
    {
        get
        {
            if (_routeRepository == null)
                _routeRepository = new RouteRepository(_db);
            return _routeRepository;
        }
    }
    
    public IVehicleRepository Vehicles
    {
        get
        {
            if (_vehicleRepository == null)
                _vehicleRepository = new VehicleRepository(_db);
            return _vehicleRepository;
        }
    }
    
    public void Save()
    {
        _db.SaveChanges();
    }

    private bool disposed = false;

    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            this.disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}