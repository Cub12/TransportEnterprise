using Moq;
using TransportEnterprise.Catalog.BLL.Services.Impl;
using TransportEnterprise.Catalog.BLL.Services.Interfaces;
using TransportEnterprise.Catalog.CCL.Security;
using TransportEnterprise.Catalog.CCL.Security.Identity;
using TransportEnterprise.Catalog.DAL.Repositories.Interfaces;
using TransportEnterprise.Catalog.DAL.UnitOfWork;

namespace TransportEnterprise.Catalog.BLL.Tests;

public class RouteServiceTests
{
    [Fact]
    public void Ctor_InputNull_ThrowsArgumentNullException()
    {
        // Arrange
        IUnitOfWork nullUnitOfWork = null;

        // Act
        // Assert
        Assert.Throws<ArgumentNullException>(() => new RouteService(nullUnitOfWork));
    }

    [Fact]
    public void GetRoutes_UserIsMechanic_ThrowMethodAccessException()
    {
        // Arrange
        User user = new Mechanic(1, "Jack", 1);
        SecurityContext.SetUser(user);
        var mockUnitOfWork = new Mock<IUnitOfWork>();
        IRouteService routeService = new RouteService(mockUnitOfWork.Object);

        // Act
        // Assert
        Assert.Throws<MethodAccessException>(() => routeService.GetRoutes());
    }

    [Fact]
    public void GetRoutes_RouteFromDAL_CorrectMappingToRouteDTO()
    {
        // Arrange
        User user = new Admin(1, "Jack", 1);
        SecurityContext.SetUser(user);
        var routeService = GetRouteService();

        // Act
        var actualRouteDTO = routeService.GetRoutes().First();

        // Assert
        Assert.True(actualRouteDTO.RouteId == 1
                    && actualRouteDTO.StartingPoint == "Kyiv"
                    && actualRouteDTO.EndPoint == "Lviv"
                    && actualRouteDTO.Length == 541
                    && actualRouteDTO.TransportEnterpriseId == 1
        );
    }

    IRouteService GetRouteService()
    {
        var mockContext = new Mock<IUnitOfWork>();
        var expectedRoute = new DAL.Entities.Route
        {
            RouteId = 1,
            StartingPoint = "Kyiv",
            EndPoint = "Lviv",
            Length = 541,
            TransportEnterpriseId = 1
        };
        var mockDbSet = new Mock<IRouteRepository>();
        mockDbSet.Setup(m => m.Find(It
                .IsAny<Func<DAL.Entities.Route, bool>>()))
            .Returns(new List<DAL.Entities.Route> { expectedRoute });
        mockContext.Setup(context => context.Routes).Returns(mockDbSet.Object);

        IRouteService routeService = new RouteService(mockContext.Object);

        return routeService;
    }
}