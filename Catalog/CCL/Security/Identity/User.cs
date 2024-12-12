namespace TransportEnterprise.Catalog.CCL.Security.Identity;

public abstract class User
{
    public User(int userId, string name, int transportEnterpriseId, string userType)
    {
        UserId = userId;
        Name = name;
        TransportEnterpriseId = transportEnterpriseId;
        UserType = userType;
    }

    public int UserId { get; }
    public string Name { get; }
    public int TransportEnterpriseId { get; }
    protected string UserType { get; }
}