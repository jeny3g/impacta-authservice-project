using Auth.Service.Application.Sessions.Commands.Authenticate;
using Auth.Service.UnitTest._Builders.Commands.Sessions;

namespace Auth.Service.UnitTest.Application.Sessions.Commands.Authenticate;

public class AuthenticateSessionCommandValidatorTest
{
    private readonly Messages _messages;
    private readonly AuthenticateSessionCommandBuilder _commandBuilder;
    private readonly Faker _faker;
    private readonly AuthenticateSessionCommandValidator _sut;

    public AuthenticateSessionCommandValidatorTest()
    {
        _messages = MessagesConfig.Build();
        _commandBuilder = AuthenticateSessionCommandBuilder.New();
        _faker = new Faker();
        _sut = new AuthenticateSessionCommandValidator(_messages);
    }

    [Theory]
    [InlineData("", Messages.Validations.NOT_EMPTY)]
    [InlineData("invalidemail@", Messages.Validations.INVALID_EMAIL_FORMAT)]
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
    [InlineData(null, Messages.Validations.NOT_EMPTY)]
    public async Task Should_Error_On_Invalid_Password(string password, string expectedError)
    {
        _commandBuilder.WithPassword(password);
        var request = _commandBuilder.Build();
        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(e => e.Password)
            .WithErrorMessage(_messages.GetMessage(expectedError));

        Assert.False(result.IsValid);
    }
}

