namespace Auth.Service.Application.Users.Commands.Create;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly Messages _messages;

    public CreateUserCommandValidator(Messages messages)
    {
        _messages = messages;

        ValidateName();
        ValidateEmail();
        ValidatePassword();
    }

    private void ValidateName()
    {
        RuleFor(e => e.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(_messages.GetMessage(Messages.Validations.NOT_EMPTY))
            .MaximumLength(50)
            .WithMessage(_messages.GetMessage(Messages.Validations.MAX_LENGTH))
            .WithErrorCode(FluentValidationErrorCode.MaxLengthValidation)
            .Must(Utils.IsValidName)
            .WithMessage(_messages.GetMessage(Messages.Validations.MUST_ONLY_CONTAIN_LETTERS));
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

    private void ValidatePassword()
    {
        RuleFor(e => e.Password)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(_messages.GetMessage(Messages.Validations.NOT_EMPTY))
            .MinimumLength(8)
            .WithMessage(_messages.GetMessage(Messages.Validations.MIN_LENGTH))
            .Must(Utils.HasValidPassword)
            .WithMessage(_messages.GetMessage(Messages.Validations.MUST_BE_STRONG_PASSWORD));
    }
}
