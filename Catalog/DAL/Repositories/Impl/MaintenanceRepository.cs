using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Impl;

public class MaintenanceRepository : BaseRepository<Maintenance>, IMaintenanceRepository
{
    internal MaintenanceRepository(TransportEnterpriseContext context) : base(context)
    {
    }

    public IEnumerable<Maintenance> GetScheduledMaintenance(DateTime date)
    {
        return Find(m => m.Date == date.Date);
    }
}