namespace Auth.Service.Application.Sessions.Commands.Authenticate;

public class AuthenticateSessionCommandValidator : AbstractValidator<AuthenticateSessionCommand>
{
    private readonly Messages _messages;

    public AuthenticateSessionCommandValidator(Messages messages)
    {
        _messages = messages;

        ValidateEmail();
        ValidatePasswordForAuthentication();
    }

    private void ValidateEmail()
    {
        RuleFor(e => e.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(_messages.GetMessage(Messages.Validations.NOT_EMPTY))
            .EmailAddress()
            .WithMessage(_messages.GetMessage(Messages.Validations.INVALID_EMAIL_FORMAT));
    }

    private void ValidatePasswordForAuthentication()
    {
        RuleFor(e => e.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(_messages.GetMessage(Messages.Validations.NOT_EMPTY));
    }
}
