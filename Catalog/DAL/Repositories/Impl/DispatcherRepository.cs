using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Impl;

public class DispatcherRepository : BaseRepository<Dispatcher>, IDispatcherRepository
{
    internal DispatcherRepository(TransportEnterpriseContext context) : base(context)
    {
    }

    public IEnumerable<Dispatcher> GetDispatchersByFullName(string fullName)
    {
        return Find(d => d.FullName != null && d.FullName.Equals(fullName,
            StringComparison.OrdinalIgnoreCase));
    }
}