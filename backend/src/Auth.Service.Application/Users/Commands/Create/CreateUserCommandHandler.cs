using Auth.Service.Mail.Interfaces;

namespace Auth.Service.Application.Users.Commands.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateSuccess>
{
	private readonly IAuthContext _context;
	private readonly IPasswordHashService _passwordHashService;
    private readonly IFluentEmailService _emailService;
    private readonly IUserVerificationTokenService  _userTokenService;

    public CreateUserCommandHandler(IAuthContext context, IPasswordHashService passwordHashService, IFluentEmailService emailService, IUserVerificationTokenService  userTokenService)
	{
		_context = context;
		_passwordHashService = passwordHashService;
        _emailService = emailService;
        _userTokenService = userTokenService;
	}

    public async Task<CreateSuccess> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(request.Email), cancellationToken);

        if (user != null && user.Active)
            throw new UserAlreadyExistsException();

        if (user is not null)
        {
            await SendVerificationEmail(user);
            return new CreateSuccess(user.Id);
        }

        return await CreateUser(request, cancellationToken);
    }

    private async Task<CreateSuccess> CreateUser(CreateUserCommand request, CancellationToken cancellationToken)
    {

        var newUser = request.Adapt<User>();
        newUser.Active = false;
        newUser.PasswordHash = _passwordHashService.HashPassword(request.Password);

        try
        {
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception dbEx)
        {
            throw new PersistenceException(dbEx);
        }

        await SendVerificationEmail(newUser);
        return new CreateSuccess(newUser.Id);
    }

    private async Task SendVerificationEmail(User user)
    {
        var token = await _userTokenService.GenerateTokenForUser(user.Id);
        var verificationUrl = _userTokenService.GetVerificationUrl(token);

        await _emailService.SendConfirmationEmailAsync(user.Email, user.Name, verificationUrl);
    }
}