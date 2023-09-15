using Auth.Service.Application.Passwords.Commands.Forgot;
using Auth.Service.UnitTest._Builders.Commands.Passwords;

namespace Auth.Service.UnitTest.Application.Passwords.Commands.ForgotPassword;

public class ForgotPasswordCommandValidatorTest
{
    private readonly Messages _messages;
    private readonly ForgotPasswordCommandBuilder _commandBuilder;
    private readonly Faker _faker;
    private readonly ForgotPasswordCommandValidator _sut;

    public ForgotPasswordCommandValidatorTest()
    {
        _messages = MessagesConfig.Build();
        _commandBuilder = ForgotPasswordCommandBuilder.New();
        _faker = new Faker();
        _sut = new ForgotPasswordCommandValidator(_messages);
    }

    [Theory]
    [InlineData("", Messages.Validations.NOT_EMPTY)]
    public async Task Should_Error_On_Empty_Email(string email, string expectedError)
    {
        _commandBuilder.WithEmail(email);
        var request = _commandBuilder.Build();
        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(e => e.Email)
            .WithErrorMessage(_messages.GetMessage(expectedError));

        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("invalidEmail", Messages.Validations.INVALID_EMAIL_FORMAT)]
    [InlineData("missingAt.com", Messages.Validations.INVALID_EMAIL_FORMAT)]
    public async Task Should_Error_On_Invalid_Email_Format(string email, string expectedError)
    {
        _commandBuilder.WithEmail(email);
        var request = _commandBuilder.Build();
        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(e => e.Email)
            .WithErrorMessage(_messages.GetMessage(expectedError));

        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task Should_Pass_With_Valid_Email()
    {
        var validEmail = _faker.Internet.Email();
        _commandBuilder.WithEmail(validEmail);

        var request = _commandBuilder.Build();
        var result = await _sut.TestValidateAsync(request);

        result.ShouldNotHaveValidationErrorFor(e => e.Email);
        Assert.True(result.IsValid);
    }
}
