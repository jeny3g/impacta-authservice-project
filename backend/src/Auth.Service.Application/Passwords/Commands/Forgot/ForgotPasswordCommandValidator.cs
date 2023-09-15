namespace Auth.Service.Application.Passwords.Commands.Forgot;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator(Messages messages)
    {
        RuleFor(e => e.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(messages.GetMessage(Messages.Validations.NOT_EMPTY))
            .EmailAddress()
            .WithMessage(messages.GetMessage(Messages.Validations.INVALID_EMAIL_FORMAT));
    }
}
