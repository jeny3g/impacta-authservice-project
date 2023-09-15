using Auth.Service.Application.Base;
using Microsoft.AspNetCore.Http;

namespace Auth.Service.Application.Addresses.Commands.Create;

public class CreateAddressCommandHandler : AuthenticatedHandler, IRequestHandler<CreateAddressCommand, CreateSuccess>
{
    private readonly IAuthContext _context;
    private readonly Messages _messages;

    public CreateAddressCommandHandler(IAuthContext context, Messages messages, IHttpContextAccessor httpContextAccessor)
        : base(httpContextAccessor)
    {
        _context = context;
        _messages = messages;
    }

    public async Task<CreateSuccess> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        EnsureUserExists();

        var customer = request.Adapt<CustomerAddress>();
        var address = request.DeliveryAddress.Adapt<Address>();

        customer.IsPrimaryAddress = await DeterminePrimaryAddressStatus(request.IsPrimaryAddress);
        customer.UserId = UserId;
        customer.DeliveryAddress = address;

        await AddAddressToDatabase(customer);

        return new CreateSuccess(customer.Id);
    }

    private async void EnsureUserExists()
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id.Equals(UserId));

        if (!userExists)
            throw new NotFoundException(_messages.GetMessage(Messages.Exception.NOT_FOUND));
    }

    private async Task<bool> DeterminePrimaryAddressStatus(bool? isPrimaryFromRequest)
    {
        if (isPrimaryFromRequest.HasValue)
            return isPrimaryFromRequest.Value;

        return !await _context.CustomerAddresses .AnyAsync(ca => ca.UserId.Equals(UserId));
    }

    private async Task AddAddressToDatabase(CustomerAddress customer)
    {
        try
        {
            _context.CustomerAddresses.Add(customer);
            customer.DeliveryAddressId = customer.DeliveryAddress.Id;
            await _context.SaveChangesAsync();
        }
        catch (Exception dbEx)
        {
            throw new PersistenceException(dbEx);
        }
    }
}
