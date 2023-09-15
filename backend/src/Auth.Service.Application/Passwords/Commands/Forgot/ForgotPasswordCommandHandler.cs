using Auth.Service.Mail.Interfaces;

namespace Auth.Service.Application.Passwords.Commands.Forgot;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, UserEmailNotRegistered>
{
    private readonly IAuthContext _context;
    private readonly IUserVerificationTokenService _userTokenService;
    private readonly IFluentEmailService _emailService;


    public ForgotPasswordCommandHandler(
        IAuthContext context,
        IUserVerificationTokenService userTokenService,
        IFluentEmailService emailService
        )
    {
        _context = context;
        _userTokenService = userTokenService;
        _emailService = emailService;
    }

    public async Task<UserEmailNotRegistered> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var user = await _context.Users.FirstOrDefaultAsync(e => e.Email == request.Email, cancellationToken);

        if (user is null)
            throw new UserEmailNotRegistered();

        var token = await _userTokenService.GenerateTokenForUser(user.Id);

        var verificationUrl = _userTokenService.GetResetPasswordUrl(token);

        await _emailService.SendPasswordResetEmailAsync(user.Email, user.Name, verificationUrl);

        throw new UserEmailNotRegistered();
    }
}