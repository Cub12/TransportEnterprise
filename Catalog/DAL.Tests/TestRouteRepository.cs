using DAL.Entities;
using DAL.Repositories.Impl;
using Microsoft.EntityFrameworkCore;

namespace DAL.Tests;

public class TestRouteRepository : BaseRepository<Route>
{
    public TestRouteRepository(DbContext context) : base(context)
    {
        
    }
}