namespace Auth.Service.Domain.Entities;

public class UserToken: TrackerDbEntity
{
    public Guid Token { get; set; }
    public Guid UserId { get; set; }
    public DateTime ExpiresAt { get; set; }

    public virtual User User { get; set; }
}