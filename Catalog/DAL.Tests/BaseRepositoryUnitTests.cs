using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Tests;

public class BaseRepositoryUnitTests
{
    [Fact]
    public void Create_InputRouteInstance_CalledAddMethodOfDBSetWithRouteInstance()
    {
        // Arrange
        DbContextOptions opt = new DbContextOptionsBuilder<TransportEnterpriseContext>().Options;
        var mockContext = new Mock<TransportEnterpriseContext>(opt);
        var mockDbSet = new Mock<DbSet<Route>>();
        mockContext.Setup(context => context.Set<Route>()).Returns(mockDbSet.Object);
        var repository = new TestRouteRepository(mockContext.Object);
        var expectedRoute = new Mock<Route>().Object;

        //Act
        repository.Create(expectedRoute);

        // Assert
        mockDbSet.Verify(dbSet => dbSet.Add(expectedRoute), Times.Once);
    }

    [Fact]
    public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
    {
        // Arrange
        DbContextOptions opt = new DbContextOptionsBuilder<TransportEnterpriseContext>().Options;
        var mockContext = new Mock<TransportEnterpriseContext>(opt);
        var mockDbSet = new Mock<DbSet<Route>>();
        mockContext.Setup(context => context.Set<Route>()).Returns(mockDbSet.Object);

        // Random id
        var expectedRoute = new Route { Id = 1 };
        mockDbSet.Setup(mock => mock.Find(expectedRoute.Id)).Returns(expectedRoute);
        var repository = new TestRouteRepository(mockContext.Object);

        // Act
        var actualRoute = repository.Get(expectedRoute.Id);

        // Assert
        mockDbSet.Verify(dbSet => dbSet.Find(expectedRoute.Id), Times.Once());
        Assert.Equal(expectedRoute, actualRoute);
    }

    [Fact]
    public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
    {
        // Arrange
        DbContextOptions opt = new DbContextOptionsBuilder<TransportEnterpriseContext>().Options;
        var mockContext = new Mock<TransportEnterpriseContext>(opt);
        var mockDbSet = new Mock<DbSet<Route>>();
        mockContext.Setup(context => context.Set<Route>()).Returns(mockDbSet.Object);
        var repository = new TestRouteRepository(mockContext.Object);
        
        var expectedRoute = new Route { Id = 1 };
        mockDbSet.Setup(mock => mock.Find(expectedRoute.Id)).Returns(expectedRoute);

        // Act
        repository.Delete(expectedRoute.Id);

        // Assert
        mockDbSet.Verify(dbSet => dbSet.Find(expectedRoute.Id), Times.Once);
        mockDbSet.Verify(dbSet => dbSet.Remove(expectedRoute), Times.Once);
    }
}