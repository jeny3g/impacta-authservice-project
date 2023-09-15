
namespace Auth.Service.Application.Passwords.Commands.Reset;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    private readonly Messages _messages;
    private readonly IBaseValidationService _validationService;

    public ResetPasswordCommandValidator(Messages messages, IBaseValidationService validationService)
    {
        _messages = messages;
        _validationService = validationService;

        ValidateToken();
        ValidatePassword();
        ValidateConfirmPassword();
    }

    private void ValidateToken()
    {
        RuleFor(e => e.Token)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(_messages.GetMessage(Messages.Validations.NOT_EMPTY))
            .MustAsync(async (token, cancellation) =>
                await _validationService.ExistsBy<UserToken>(t => t.Token.Equals(token)))
            .WithMessage(_messages.GetMessage(Messages.Validations.USERTOKEN_TOKEN_NOT_VALID))
            .WithErrorCode(FluentValidationErrorCode.EntityExistsValidation);
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

    private void ValidateConfirmPassword()
    {
        RuleFor(e => e.ConfirmPassword)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(_messages.GetMessage(Messages.Validations.NOT_EMPTY))
            .Equal(e => e.Password)
            .WithMessage(_messages.GetMessage(Messages.Validations.PASSWORD_MISMATCH));
    }
}
