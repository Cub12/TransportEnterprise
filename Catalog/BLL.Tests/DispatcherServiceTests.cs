using Moq;
using TransportEnterprise.Catalog.BLL.Services.Impl;
using TransportEnterprise.Catalog.BLL.Services.Interfaces;
using TransportEnterprise.Catalog.CCL.Security;
using TransportEnterprise.Catalog.CCL.Security.Identity;
using TransportEnterprise.Catalog.DAL.Repositories.Interfaces;
using TransportEnterprise.Catalog.DAL.UnitOfWork;

namespace TransportEnterprise.Catalog.BLL.Tests;

public class DispatcherServiceTests
{
    [Fact]
    public void Ctor_InputNull_ThrowsArgumentNullException()
    {
        // Arrange
        IUnitOfWork nullUnitOfWork = null;

        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new DispatcherService(nullUnitOfWork));
    }

    [Fact]
    public void GetDispatchers_UserIsMechanic_ThrowMethodAccessException()
    {
        // Arrange
        User user = new Mechanic(1, "Jack", 1);
        SecurityContext.SetUser(user);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        IDispatcherService dispatcherService = new DispatcherService(mockUnitOfWork.Object);

        // Act
        // Assert
        Assert.Throws<MethodAccessException>(() => dispatcherService.GetDispatchers());
    }

    [Fact]
    public void GetDispatchers_DispatcherFromDAL_CorrectMappingToDispatcherDTO()
    {
        // Arrange
        User user = new Admin(1, "Jack", 1);
        SecurityContext.SetUser(user);
        var dispatcherService = GetDispatcherService();

        // Act
        var actualDispatcherDTO = dispatcherService.GetDispatchers().First();

        // Assert
        Assert.True(actualDispatcherDTO.DispatcherId == 1 && actualDispatcherDTO.FullName == "John Doe"
                                                          && actualDispatcherDTO.TransportEnterpriseId == 1
        );
    }

    IDispatcherService GetDispatcherService()
    {
        var mockContext = new Mock<IUnitOfWork>();
        var expectedDispatcher = new TransportEnterprise.Catalog.DAL.Entities.Dispatcher
        {
            DispatcherId = 1,
            FullName = "John Doe",
            TransportEnterpriseId = 1
        };
        var mockDbSet = new Mock<IDispatcherRepository>();
        mockDbSet.Setup(d => d.Find(It
                .IsAny<Func<TransportEnterprise.Catalog.DAL.Entities.Dispatcher, bool>>()))
            .Returns(new List<TransportEnterprise.Catalog.DAL.Entities.Dispatcher> { expectedDispatcher });
        mockContext.Setup(context => context.Dispatchers).Returns(mockDbSet.Object);

        IDispatcherService dispatcherService = new DispatcherService(mockContext.Object);

        return dispatcherService;
    }
}