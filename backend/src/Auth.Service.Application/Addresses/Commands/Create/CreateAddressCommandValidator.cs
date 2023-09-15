namespace Auth.Service.Application.Addresses.Commands.Create;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    private readonly Messages _messages;

    public CreateAddressCommandValidator(Messages messages)
    {
        _messages = messages;

        ValidateReceiverName();
        ValidatePhone();
        ValidateDeliveryAddress();
        ValidateStreet();
        ValidateCity();
        ValidateState();
        ValidateZipCode();
        ValidateCountry();
        ValidateNeighborhood();
        ValidateStreetNumber();
        ValidateComplement();
    }

    private void ValidateReceiverName()
    {
        RuleFor(e => e.ReceiverName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(_messages.GetMessage(Messages.Validations.NOT_EMPTY))
            .MaximumLength(50)
            .WithMessage(_messages.GetMessage(Messages.Validations.MAX_LENGTH))
            .WithErrorCode(FluentValidationErrorCode.MaxLengthValidation)
            .Must(Utils.IsValidName)
            .WithMessage(_messages.GetMessage(Messages.Validations.MUST_ONLY_CONTAIN_LETTERS));
    }

    private void ValidatePhone()
    {
        RuleFor(e => e.Phone)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(_messages.GetMessage(Messages.Validations.NOT_EMPTY))
            .MaximumLength(50)
            .WithMessage(_messages.GetMessage(Messages.Validations.MAX_LENGTH))
            .WithErrorCode(FluentValidationErrorCode.MaxLengthValidation)
            .Must(Utils.IsValidPhone)
            .WithMessage(_messages.GetMessage(Messages.Validations.INVALID_PHONE_FORMAT));
    }

    private void ValidateDeliveryAddress()
    {
        RuleFor(e => e.DeliveryAddress)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage(_messages.GetMessage(Messages.Validations.NOT_EMPTY))
            .WithErrorCode(FluentValidationErrorCode.NotEmptyValidation);
    }

    private void ValidateStreet()
    {
        RuleFor(e => e.DeliveryAddress.Street)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(_messages.GetMessage(Messages.Validations.NOT_EMPTY))
            .MaximumLength(100)
            .WithMessage(_messages.GetMessage(Messages.Validations.MAX_LENGTH))
            .WithErrorCode(FluentValidationErrorCode.MaxLengthValidation);
    }

    private void ValidateCity()
    {
        RuleFor(e => e.DeliveryAddress.City)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(_messages.GetMessage(Messages.Validations.NOT_EMPTY))
            .MaximumLength(50)
            .WithMessage(_messages.GetMessage(Messages.Validations.MAX_LENGTH))
            .WithErrorCode(FluentValidationErrorCode.MaxLengthValidation);
    }

    private void ValidateState()
    {
        RuleFor(e => e.DeliveryAddress.State)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(50)
            .WithMessage(_messages.GetMessage(Messages.Validations.MAX_LENGTH))
            .WithErrorCode(FluentValidationErrorCode.MaxLengthValidation);
    }

    private void ValidateZipCode()
    {
        RuleFor(e => e.DeliveryAddress.ZipCode)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(_messages.GetMessage(Messages.Validations.NOT_EMPTY))
            .MaximumLength(8)
            .WithMessage(_messages.GetMessage(Messages.Validations.MAX_LENGTH))
            .WithErrorCode(FluentValidationErrorCode.MaxLengthValidation)
            .Must(Utils.IsValidZipCode)
            .WithMessage(_messages.GetMessage(Messages.Validations.INVALID_ZIP_CODE_FORMAT));
    }

    private void ValidateCountry()
    {
        RuleFor(e => e.DeliveryAddress.Country)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(_messages.GetMessage(Messages.Validations.NOT_EMPTY))
            .MaximumLength(3)
            .WithMessage(_messages.GetMessage(Messages.Validations.MAX_LENGTH))
            .WithErrorCode(FluentValidationErrorCode.MaxLengthValidation);
    }

    private void ValidateNeighborhood()
    {
        RuleFor(e => e.DeliveryAddress.Neighborhood)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(50)
            .WithMessage(_messages.GetMessage(Messages.Validations.MAX_LENGTH))
            .WithErrorCode(FluentValidationErrorCode.MaxLengthValidation);
    }

    private void ValidateStreetNumber()
    {
        RuleFor(e => e.DeliveryAddress.StreetNumber)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(15)
            .WithMessage(_messages.GetMessage(Messages.Validations.MAX_LENGTH))
            .WithErrorCode(FluentValidationErrorCode.MaxLengthValidation);
    }

    private void ValidateComplement()
    {
        RuleFor(e => e.DeliveryAddress.Complement)
            .Cascade(CascadeMode.Stop)
            .MaximumLength(100)
            .WithMessage(_messages.GetMessage(Messages.Validations.MAX_LENGTH))
            .WithErrorCode(FluentValidationErrorCode.MaxLengthValidation);
    }
}
