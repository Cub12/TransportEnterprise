namespace TransportEnterprise.Catalog.CCL.Security.Identity;

public class Dispatcher : User
{
    public Dispatcher(int userId, string name, int transportEnterpriseId) : 
        base(userId, name, transportEnterpriseId, nameof(Dispatcher))
    {
    }
}