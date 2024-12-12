namespace TransportEnterprise.Catalog.CCL.Security.Identity;

public class Admin : User
{
    public Admin(int userId, string name, int transportEnterpriseId) :
        base(userId, name, transportEnterpriseId, nameof(Admin))
    {
    }
}