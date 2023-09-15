using Auth.Service.Application.Sessions.Models;

namespace Auth.Service.Application.Sessions.Commands.Authenticate;

public class AuthenticateSessionCommandHandler : IRequestHandler<AuthenticateSessionCommand, AuthenticateSessionViewModel>
{
    private readonly IAuthContext _context;
    private readonly IPasswordHashService _passwordHashService;
    private readonly IJwtAuthenticationService _jwtAuthenticationService;
    private readonly Messages _messages;

    public AuthenticateSessionCommandHandler(
        IAuthContext context, 
        Messages messages,
        IPasswordHashService passwordHashService, 
        IJwtAuthenticationService jwtAuthenticationService)
    {
        _context = context;
        _messages = messages;
        _passwordHashService = passwordHashService;
        _jwtAuthenticationService = jwtAuthenticationService;
    }

    public async Task<AuthenticateSessionViewModel> Handle(AuthenticateSessionCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower().Equals(request.Email.ToLower()));

        if (user is null)
            throw new NotFoundException(_messages.GetMessage(Messages.Exception.INVALID_EMAIL_OR_PASSWORD));

        if (!_passwordHashService.VerifyPassword(user.PasswordHash, request.Password))
            throw new NotFoundException(_messages.GetMessage(Messages.Exception.INVALID_EMAIL_OR_PASSWORD));

        if (!user.Active)
            throw new ForbiddenException(_messages.GetMessage(Messages.Exception.USER_NOT_ACTIVE));

        string token = _jwtAuthenticationService.GenerateJWT(user);

        return new AuthenticateSessionViewModel()
        {
            Token = token
        };
    }
}

