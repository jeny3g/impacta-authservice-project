using Auth.Service.Application.Users.Commands.Create;
using Auth.Service.UnitTest._Builders.Commands.Users;

namespace Auth.Service.UnitTest.Application.Users.Commands.Create;

public class CreateUserCommandValidatorTest
{
    private readonly Messages _messages;
    private readonly CreateUserCommandBuilder _commandBuilder;
    private readonly Faker _faker;
    private readonly CreateUserCommandValidator _sut;

    public CreateUserCommandValidatorTest()
    {
        _messages = MessagesConfig.Build();
        _commandBuilder = CreateUserCommandBuilder.New();
        _faker = new Faker();
        _sut = new CreateUserCommandValidator(_messages);
    }

    [Theory]
    [InlineData("", Messages.Validations.NOT_EMPTY)]
    [InlineData("John123", Messages.Validations.MUST_ONLY_CONTAIN_LETTERS)]
    [InlineData(null, Messages.Validations.NOT_EMPTY)]  // Assuming null names should be caught
    public async Task Should_Error_On_Invalid_Name(string name, string expectedError)
    {
        _commandBuilder.WithName(name);
        var request = _commandBuilder.Build();
        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(e => e.Name)
            .WithErrorMessage(_messages.GetMessage(expectedError));

        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("", Messages.Validations.NOT_EMPTY)]
    [InlineData("invalidemail", Messages.Validations.INVALID_EMAIL_FORMAT)]
    [InlineData(null, Messages.Validations.NOT_EMPTY)]
    public async Task Should_Error_On_Invalid_Email(string email, string expectedError)
    {
        _commandBuilder.WithEmail(email);
        var request = _commandBuilder.Build();
        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(e => e.Email)
            .WithErrorMessage(_messages.GetMessage(expectedError));

        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("", Messages.Validations.NOT_EMPTY)]
    [InlineData("weakpass", Messages.Validations.MUST_BE_STRONG_PASSWORD)]
    public async Task Should_Error_On_Invalid_Password(string password, string expectedError)
    {
        _commandBuilder.WithPassword(password);
        var request = _commandBuilder.Build();
        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(e => e.Password)
            .WithErrorMessage(_messages.GetMessage(expectedError));

        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task Should_Error_When_Name_Is_Longer_Than_50_Characters()
    {
        _commandBuilder.WithName(_faker.Random.String(51));
        var request = _commandBuilder.Build();
        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(e => e.Name)
            .WithErrorCode(FluentValidationErrorCode.MaxLengthValidation)
            .WithErrorMessage(_messages.GetMessage(Messages.Validations.MAX_LENGTH).Replace("{MaxLength}", "50"));

        Assert.False(result.IsValid);

    }

    [Fact]
    public async Task Should_Error_When_Password_Is_Less_Than_8_Characters()
    {
        _commandBuilder.WithPassword(_faker.Random.String(7));
        var request = _commandBuilder.Build();
        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(e => e.Password)
            .WithErrorMessage(_messages.GetMessage(Messages.Validations.MIN_LENGTH).Replace("{MinLength}", "8"));

        Assert.False(result.IsValid);
    }
}
