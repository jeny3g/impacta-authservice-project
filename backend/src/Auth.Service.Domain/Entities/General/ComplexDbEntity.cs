namespace Auth.Service.Domain.Entities.General;

public abstract class ComplexDbEntity : DbEntity
{
    public Guid Id { get; set; }
}
