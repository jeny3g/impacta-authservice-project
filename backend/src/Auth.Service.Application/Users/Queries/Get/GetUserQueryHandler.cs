using Auth.Service.Application.Users.Models;

namespace Auth.Service.Application.Users.Queries.Get;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserViewModel>
{
    private readonly IAuthContext _context;
    private readonly Messages _messages;

    public GetUserQueryHandler(IAuthContext context, Messages messages)
    {
        _context = context;
        _messages = messages;
    }

    public async Task<UserViewModel> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        request ??= new GetUserQuery();

        var item = await _context
            .Users
            .Where(u => u.Id.Equals(request.Id))
            .ProjectToType<UserViewModel>()
            .FirstOrDefaultAsync(cancellationToken);

        if (item is null)
            throw new NotFoundException(_messages.GetMessage(Messages.Entities.USER), request.Id);

        return item;
    }
}