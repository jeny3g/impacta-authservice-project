using Auth.Service.Application.Addresses.Models;
using Auth.Service.Application.Base;
using Microsoft.AspNetCore.Http;

namespace Auth.Service.Application.Addresses.Queries.List;

public class ListAddressQuery : IRequest<List<AddressViewModel>> { }

public class ListAddressQueryHandler : AuthenticatedHandler, IRequestHandler<ListAddressQuery, List<AddressViewModel>>
{
    public readonly IAuthContext _context;

    public ListAddressQueryHandler(IAuthContext context, IHttpContextAccessor httpContextAccessor)
    : base(httpContextAccessor)
    {
        _context = context;
    }

    public async Task<List<AddressViewModel>> Handle(ListAddressQuery request, CancellationToken cancellationToken)
    {
        var userId = UserId;
        var items = await _context.CustomerAddresses 
                .Where(ca => ca.UserId.Equals(userId))
                .Where(ca => ca.Active)
                .ProjectToType<AddressViewModel>()
                .ToListAsync(cancellationToken);

        return items;
    }
}