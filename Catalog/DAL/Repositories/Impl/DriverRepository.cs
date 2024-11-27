using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Impl;

public class DriverRepository : BaseRepository<Driver>, IDriverRepository
{
    internal DriverRepository(TransportEnterpriseContext context) : base(context)
    {
    }

    public IEnumerable<Driver> GetDriversByCardNumber(string cardNumber)
    {
        return Find(d => d.CardNumber == cardNumber);
    }
}