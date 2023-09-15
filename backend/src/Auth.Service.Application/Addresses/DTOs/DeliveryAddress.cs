namespace Auth.Service.Application.Addresses.DTOs;

public class DeliveryAddress
{
    public string ZipCode { get; set; }

    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    public string Street { get; set; }
    public string Neighborhood { get; set; }

    public string StreetNumber { get; set; }
    public string Complement { get; set; }
}
