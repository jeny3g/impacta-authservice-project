namespace Auth.Service.Domain.Entities.General;

public class TrackerDbEntity : ComplexDbEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
