namespace Auth.Service.Domain.Entities;

public class User : TrackerDbEntity
{
    public User()
    {
        UserAuthProviders = new HashSet<UserAuthProvider>();
        Employees = new HashSet<Employee>();
        UserTokens = new HashSet<UserToken>();
        CustomerAddresses = new HashSet<CustomerAddress>();
    }

    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool Active { get; set; }

    public virtual ICollection<UserAuthProvider> UserAuthProviders { get; set; }
    public virtual ICollection<Employee> Employees { get; set; }
    public virtual ICollection<UserToken> UserTokens { get; set; }
    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; }
}