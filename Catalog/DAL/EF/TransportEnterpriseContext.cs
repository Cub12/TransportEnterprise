using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF;

public class TransportEnterpriseContext : DbContext
{
    public DbSet<Dispatcher> Dispatchers { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Maintenance> Maintenances { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Vehicle> Vehicles { get; set; }

    public TransportEnterpriseContext(DbContextOptions options) : base(options)
    {
    }
}