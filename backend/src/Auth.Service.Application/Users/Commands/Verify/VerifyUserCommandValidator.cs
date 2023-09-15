namespace Auth.Service.Application.Users.Commands.Verify;

public class VerifyUserCommandValidator : AbstractValidator<VerifyUserCommand>
{
    public VerifyUserCommandValidator(Messages messages, IBaseValidationService validationService)
    {
        RuleFor(e => e.Token)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(messages.GetMessage(Messages.Validations.NOT_EMPTY))
            .MustAsync(async (token, cancellation) =>
                await validationService.ExistsBy<UserToken>(t => t.Token.Equals(token)))
            .WithMessage(messages.GetMessage(Messages.Validations.USERTOKEN_TOKEN_NOT_VALID))
            .WithErrorCode(FluentValidationErrorCode.EntityExistsValidation);
    }
}
