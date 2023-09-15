namespace Auth.Service.Domain.Entities;

public class UserAuthProvider
{
    public string ExternalUserId { get; set; }

    public Guid UserId { get; set; }
    public Guid AuthProviderId { get; set; }

    public virtual User User { get; set; }
    public virtual AuthProvider AuthProvider { get; set; }
}
