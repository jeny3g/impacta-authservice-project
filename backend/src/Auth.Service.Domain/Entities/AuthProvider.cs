namespace Auth.Service.Domain.Entities;

public class AuthProvider
{
    public AuthProvider()
    {
        UserAuthProviders = new HashSet<UserAuthProvider>();
    }

    public Guid Id { get; set; }

    public string Name { get; set; }

    public virtual ICollection<UserAuthProvider> UserAuthProviders { get; set; }
}