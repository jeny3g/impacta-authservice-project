namespace Auth.Service.Domain.Entities;

public class Employee : TrackerDbEntity
{
    public string Name { get; set; }
    public string Phone { get; set; }
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public bool Active { get; set; }

    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
}