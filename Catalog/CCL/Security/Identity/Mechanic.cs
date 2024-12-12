namespace TransportEnterprise.Catalog.CCL.Security.Identity;

public class Mechanic : User
{
    public Mechanic(int userId, string name, int transportEnterpriseId) : 
        base(userId, name, transportEnterpriseId,nameof(Mechanic))
    {
    }
}