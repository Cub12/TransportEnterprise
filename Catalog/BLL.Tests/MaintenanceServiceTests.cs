using Moq;
using TransportEnterprise.Catalog.BLL.Services.Impl;
using TransportEnterprise.Catalog.BLL.Services.Interfaces;
using TransportEnterprise.Catalog.CCL.Security;
using TransportEnterprise.Catalog.CCL.Security.Identity;
using TransportEnterprise.Catalog.DAL.Repositories.Interfaces;
using TransportEnterprise.Catalog.DAL.UnitOfWork;

namespace TransportEnterprise.Catalog.BLL.Tests;

public class MaintenanceServiceTests
{
    [Fact]
    public void Ctor_InputNull_ThrowsArgumentNullException()
    {
        // Arrange
        IUnitOfWork nullUnitOfWork = null;

        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new MaintenanceService(nullUnitOfWork));
    }

    [Fact]
    public void GetMaintenances_UserIsDriver_ThrowMethodAccessException()
    {
        // Arrange
        User user = new Driver(1, "Jack", 1);
        SecurityContext.SetUser(user);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        IMaintenanceService maintenanceService = new MaintenanceService(mockUnitOfWork.Object);

        // Act
        // Assert
        Assert.Throws<MethodAccessException>(() => maintenanceService.GetMaintenances());
    }

    [Fact]
    public void GetMaintenances_MaintenanceFromDAL_CorrectMappingToMaintenanceDTO()
    {
        // Arrange
        User user = new Admin(1, "Jack", 1);
        SecurityContext.SetUser(user);
        var maintenanceService = GetMaintenanceService();

        // Act
        var actualMaintenanceDTO = maintenanceService.GetMaintenances().First();

        // Assert
        Assert.True(actualMaintenanceDTO.MaintenanceId == 1
                    && actualMaintenanceDTO.Date == new DateTime(2020, 1, 1)
                    && actualMaintenanceDTO.ServiceType == "Repairing"
                    && actualMaintenanceDTO.Description == "Chassis replacement"
                    && actualMaintenanceDTO.TransportEnterpriseId == 1
        );
    }

    IMaintenanceService GetMaintenanceService()
    {
        var mockContext = new Mock<IUnitOfWork>();
        var expectedMaintenance = new DAL.Entities.Maintenance
        {
            MaintenanceId = 1,
            Date = new DateTime(2020, 1, 1),
            ServiceType = "Repairing",
            Description = "Chassis replacement",
            TransportEnterpriseId = 1
        };
        var mockDbSet = new Mock<IMaintenanceRepository>();
        mockDbSet.Setup(m => m.Find(It
                .IsAny<Func<DAL.Entities.Maintenance, bool>>()))
            .Returns(new List<DAL.Entities.Maintenance> { expectedMaintenance });
        mockContext.Setup(context => context.Maintenances).Returns(mockDbSet.Object);

        IMaintenanceService maintenanceService = new MaintenanceService(mockContext.Object);

        return maintenanceService;
    }
}