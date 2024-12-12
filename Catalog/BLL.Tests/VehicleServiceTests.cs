using Moq;
using TransportEnterprise.Catalog.BLL.Services.Impl;
using TransportEnterprise.Catalog.BLL.Services.Interfaces;
using TransportEnterprise.Catalog.CCL.Security;
using TransportEnterprise.Catalog.CCL.Security.Identity;
using TransportEnterprise.Catalog.DAL.Entities;
using TransportEnterprise.Catalog.DAL.Repositories.Interfaces;
using TransportEnterprise.Catalog.DAL.UnitOfWork;

namespace TransportEnterprise.Catalog.BLL.Tests;

public class VehicleServiceTests
{
    [Fact]
    public void Ctor_InputNull_ThrowsArgumentNullException()
    {
        // Arrange
        IUnitOfWork nullUnitOfWork = null;

        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new VehicleService(nullUnitOfWork));
    }

    [Fact]
    public void GetVehicles_VehicleFromDAL_CorrectMappingToVehicleDTO()
    {
        // Arrange
        User user = new Admin(1, "Jack", 1);
        SecurityContext.SetUser(user);
        var vehicleService = GetVehicleService();
        
        // Act
        var actualVehicleDTO = vehicleService.GetVehicles().First();

        // Assert
        Assert.True(actualVehicleDTO.VehicleId == 1 && actualVehicleDTO.LicensePlate == "2222"
                                                    && actualVehicleDTO.Model == "Mini Countryman"
                                                    && actualVehicleDTO.ProductionYear == 2015
                                                    && actualVehicleDTO.Type == "Crossover"
                                                    && actualVehicleDTO.TransportEnterpriseId == 1
        );
    }

    IVehicleService GetVehicleService()
    {
        var mockContext = new Mock<IUnitOfWork>();
        var expectedVehicle = new Vehicle()
        {
            VehicleId = 1,
            LicensePlate = "2222",
            Model = "Mini Countryman",
            ProductionYear = 2015,
            Type = "Crossover",
            TransportEnterpriseId = 1
        };
        var mockDbSet = new Mock<IVehicleRepository>();
        mockDbSet.Setup(v => v.Find(It.IsAny<Func<Vehicle, bool>>()))
            .Returns(new List<Vehicle>() {expectedVehicle});
        mockContext.Setup(context => context.Vehicles).Returns(mockDbSet.Object);
        
        IVehicleService vehicleService = new VehicleService(mockContext.Object);
        
        return vehicleService;
    }
}