namespace Auth.Service.Domain.Entities;

public class CustomerAddress : TrackerDbEntity
{
    public Guid UserId { get; set; }
    public Guid DeliveryAddressId { get; set; }

    public string ReceiverName { get; set; }
    public string Phone { get; set; }
    public bool Active { get; set; }
    public bool IsPrimaryAddress { get; set; }

    public virtual User User { get; set; }
    public virtual Address DeliveryAddress { get; set; }
}