using Auth.Service.Application.Addresses.DTOs;

namespace Auth.Service.Application.Addresses.Models;

public class AddressViewModel : ComplexDbEntityViewModel
{
    public bool? IsPrimaryAddress { get; set; }

    public string ReceiverName { get; set; }

    public string Phone { get; set; }

    public bool Active { get; set; }

    public DeliveryAddress DeliveryAddress { get; set; }
}
