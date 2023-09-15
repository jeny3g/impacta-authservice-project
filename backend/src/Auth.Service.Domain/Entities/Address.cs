namespace Auth.Service.Domain.Entities;

public class Address : TrackerDbEntity
{
    public Guid CustomerAddressId { get; set; }

    public string ZipCode { get; set; }

    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public string Street { get; set; }
    public string Neighborhood { get; set; }

    public string StreetNumber { get; set; }
    public string Complement { get; set; }

    public virtual CustomerAddress CustomerAddress { get; set; }
}
