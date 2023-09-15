namespace Auth.Service.Application.Users.Commands.Verify;

public class VerifyUserCommandHandler : IRequestHandler<VerifyUserCommand, CreateSuccess>
{
    private readonly IAuthContext _context;
    private readonly IUserVerificationTokenService _userTokenService;
    private readonly Messages _messages;

    public VerifyUserCommandHandler(IAuthContext context, Messages messages, IUserVerificationTokenService userTokenService)
    {
        _context = context;
        _messages = messages;
        _userTokenService = userTokenService;
    }

    public async Task<CreateSuccess> Handle(VerifyUserCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var userToken = await _userTokenService.GetUserTokenByToken(request.Token);

        if (userToken.User is null)
            throw new NotFoundException(_messages.GetMessage(Messages.Exception.NOT_FOUND));

        if (userToken.ExpiresAt < DateTime.UtcNow)
            throw new NotFoundException(_messages.GetMessage(Messages.Validations.USERTOKEN_TOKEN_NOT_VALID));

        userToken.User.Active = true;

        try
        {
            _context.UserTokens.Update(userToken);
            _context.Users.Update(userToken.User);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception dbEx)
        {
            throw new PersistenceException(dbEx);
        }

        return new CreateSuccess(userToken.User.Id);
    }
}

