namespace TransportEnterprise.Catalog.CCL.Security.Identity;

public class Driver : User
{
    public Driver(int userId, string name, int transportEnterpriseId) : 
        base(userId, name, transportEnterpriseId, nameof(Driver))
    {
    }
}