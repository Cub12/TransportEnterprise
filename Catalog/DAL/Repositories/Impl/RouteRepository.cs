using DAL.EF;
using DAL.Entities;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories.Impl;

public class RouteRepository : BaseRepository<Route>, IRouteRepository
{
    internal RouteRepository(TransportEnterpriseContext context) : base(context)
    {
    }

    public IEnumerable<Route> GetRouteStartingPoint(string startingPoint)
    {
        return Find(r => r.StartingPoint != null && r.StartingPoint.Equals(startingPoint, 
            StringComparison.OrdinalIgnoreCase));
    }
    
    public IEnumerable<Route> GetRouteEndPoint(string endPoint)
    {
        return Find(r => r.EndPoint != null && r.EndPoint.Equals(endPoint, StringComparison.OrdinalIgnoreCase));
    }
}