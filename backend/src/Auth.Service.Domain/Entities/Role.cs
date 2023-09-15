namespace Auth.Service.Domain.Entities;

public class Role: TrackerDbEntity  
{
    public Role()
    {
        Employees = new HashSet<Employee>();
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public bool Active { get; set; }

    public virtual ICollection<Employee> Employees { get; set; }
}
