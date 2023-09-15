using Auth.Service.Application.Addresses.DTOs;

namespace Auth.Service.Application.Addresses.Commands.Create;

public class CreateAddress
{
    public bool? IsPrimaryAddress { get; set; }

    public string ReceiverName { get; set; }

    public string Phone { get; set; }

    public bool Active { get; set; } = true;

    public DeliveryAddress DeliveryAddress { get; set; }
}

public class CreateAddressCommand : IRequest<CreateSuccess>
{
    public bool? IsPrimaryAddress { get; set; }

    public string ReceiverName { get; set; }

    public string Phone { get; set; }

    public bool Active { get; set; } = true;

    public DeliveryAddress DeliveryAddress { get; set; }
}