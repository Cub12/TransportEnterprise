using Moq;
using TransportEnterprise.Catalog.BLL.Services.Impl;
using TransportEnterprise.Catalog.BLL.Services.Interfaces;
using TransportEnterprise.Catalog.CCL.Security;
using TransportEnterprise.Catalog.CCL.Security.Identity;
using TransportEnterprise.Catalog.DAL.Repositories.Interfaces;
using TransportEnterprise.Catalog.DAL.UnitOfWork;

namespace TransportEnterprise.Catalog.BLL.Tests;

public class DriverServiceTests
{
    [Fact]
    public void Ctor_InputNull_ThrowsArgumentNullException()
    {
        // Arrange
        IUnitOfWork nullUnitOfWork = null;

        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new DriverService(nullUnitOfWork));
    }

    [Fact]
    public void GetDrivers_UserIsMechanic_ThrowMethodAccessException()
    {
        // Arrange
        User user = new Mechanic(1, "Jack", 1);
        SecurityContext.SetUser(user);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        IDriverService driverService = new DriverService(mockUnitOfWork.Object);

        // Act
        // Assert
        Assert.Throws<MethodAccessException>(() => driverService.GetDrivers());
    }

    [Fact]
    public void GetDrivers_DriverFromDAL_CorrectMappingToDriverDTO()
    {
        // Arrange
        User user = new Admin(1, "Jack", 1);
        SecurityContext.SetUser(user);
        var driverService = GetDriverService();

        // Act
        var actualDriverDTO = driverService.GetDrivers().First();

        // Assert
        Assert.True(actualDriverDTO.DriverId == 1 
                    && actualDriverDTO.FullName == "John Doe" 
                    && actualDriverDTO.CardNumber == "2222" 
                    && actualDriverDTO.BirthDate == new DateTime(1997, 12, 20) 
                    && actualDriverDTO.TransportEnterpriseId == 1
        );
    }

    IDriverService GetDriverService()
    {
        var mockContext = new Mock<IUnitOfWork>();
        var expectedDriver = new DAL.Entities.Driver()
        {
            DriverId = 1,
            FullName = "John Doe",
            CardNumber = "2222",
            BirthDate = new DateTime(1997, 12, 20),
            TransportEnterpriseId = 1
        };
        var mockDbSet = new Mock<IDriverRepository>();
        mockDbSet.Setup(d => d.Find(It
                .IsAny<Func<DAL.Entities.Driver, bool>>()))
            .Returns(new List<DAL.Entities.Driver> { expectedDriver });
        mockContext.Setup(context => context.Drivers).Returns(mockDbSet.Object);

        IDriverService driverService = new DriverService(mockContext.Object);

        return driverService;
    }
}