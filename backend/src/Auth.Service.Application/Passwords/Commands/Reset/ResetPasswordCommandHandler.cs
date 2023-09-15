namespace Auth.Service.Application.Passwords.Commands.Reset;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, CreateSuccess>
{
    private readonly IAuthContext _context;
    private readonly Messages _messages;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IUserVerificationTokenService  _userTokenService;

    public ResetPasswordCommandHandler(IAuthContext context, Messages messages, IPasswordHashService passwordHashService, IUserVerificationTokenService  userTokenService)
    {
        _context = context;
        _messages = messages;
        _passwordHashService = passwordHashService;
        _userTokenService = userTokenService;
    }

    public async Task<CreateSuccess> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var user = await _userTokenService.GetUserByToken(request.Token);

        if (user is null)
            throw new NotFoundException(_messages.GetMessage(Messages.Exception.NOT_FOUND));

        try
        {
            user.PasswordHash = _passwordHashService.HashPassword(request.Password);
            user.Active = true;

            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new PersistenceException(ex);
        }

        return new CreateSuccess(user.Id);
    }
}

