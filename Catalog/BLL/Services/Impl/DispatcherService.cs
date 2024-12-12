using AutoMapper;
using TransportEnterprise.Catalog.BLL.DTO;
using TransportEnterprise.Catalog.BLL.Services.Interfaces;
using TransportEnterprise.Catalog.CCL.Security;
using TransportEnterprise.Catalog.CCL.Security.Identity;
using TransportEnterprise.Catalog.DAL.UnitOfWork;
using Dispatcher = TransportEnterprise.Catalog.DAL.Entities.Dispatcher;
using Driver = TransportEnterprise.Catalog.DAL.Entities.Driver;

namespace TransportEnterprise.Catalog.BLL.Services.Impl;

public class DispatcherService : IDispatcherService
{
    private readonly IUnitOfWork _database;

    public DispatcherService(IUnitOfWork unitOfWork)
    {
        if (unitOfWork == null)
        {
            throw new ArgumentNullException(nameof(unitOfWork));
        }

        _database = unitOfWork;
    }

    public IEnumerable<DispatcherDTO> GetDispatchers()
    {
        var user = SecurityContext.GetUser();
        var userType = user.GetType();
        if (userType != typeof(Admin) && userType != typeof(Driver) && userType != typeof(Dispatcher))
        {
            throw new MethodAccessException();
        }
        var transportEnterpriseId = user.TransportEnterpriseId;
        var dispatchersEntities = _database.Dispatchers
            .Find(d => d.TransportEnterpriseId == transportEnterpriseId);
        var mapper = new MapperConfiguration(cfg => cfg
            .CreateMap<Dispatcher, DispatcherDTO>()).CreateMapper();
        var dispatchersDTO = mapper
            .Map<IEnumerable<Dispatcher>, List<DispatcherDTO>>(dispatchersEntities);
        return dispatchersDTO;
    }
}