using Auth.Service.Application.Passwords.Commands.Reset;
using Auth.Service.UnitTest._Builders.Commands.Passwords;
using System.Linq.Expressions;
using Neleus.LambdaCompare;

namespace Auth.Service.UnitTest.Application.Passwords.Commands.Reset;

public class ResetPasswordCommandValidatorTest
{
    private readonly Messages _messages;
    private readonly IBaseValidationService _validationService;
    private readonly ResetPasswordCommandBuilder _commandBuilder;
    private readonly Faker _faker;
    private readonly ResetPasswordCommandValidator _sut;

    public ResetPasswordCommandValidatorTest()
    {
        _messages = MessagesConfig.Build();
        _validationService = Substitute.For<IBaseValidationService>();
        _commandBuilder = ResetPasswordCommandBuilder.New();
        _faker = new Faker();
        _sut = new ResetPasswordCommandValidator(_messages, _validationService);
    }

    [Fact]
    public async Task Should_Error_When_Token_Is_Invalid()
    {
        var nonExistentToken = Guid.NewGuid();
        _commandBuilder.WithToken(nonExistentToken);

        var request = _commandBuilder.Build();

        _validationService.ExistsBy<UserToken>(Arg.Is<Expression<Func<UserToken, bool>>>(expr => Lambda.Eq(expr, e => e.Token.Equals(request.Token))))
            .Returns(false);

        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(e => e.Token)
            .WithErrorMessage(_messages.GetMessage(Messages.Validations.USERTOKEN_TOKEN_NOT_VALID))
            .WithErrorCode(FluentValidationErrorCode.EntityExistsValidation);

        Assert.False(result.IsValid);
    }

    [Theory]
    [InlineData("00000000-0000-0000-0000-000000000000", Messages.Validations.NOT_EMPTY)]  // Empty Guid
    public async Task Should_Error_On_Empty_Token(string tokenString, string expectedError)
    {
        Guid token = Guid.Parse(tokenString);
        _commandBuilder.WithToken(token);
        var request = _commandBuilder.Build();
        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(e => e.Token)
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
    public async Task Should_Error_When_Password_Is_Less_Than_8_Characters()
    {
        _commandBuilder.WithPassword(_faker.Random.String(7));
        var request = _commandBuilder.Build();
        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(e => e.Password)
            .WithErrorMessage(_messages.GetMessage(Messages.Validations.MIN_LENGTH).Replace("{MinLength}", "8"));

        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task Should_Error_When_ConfirmPassword_Does_Not_Match_Password()
    {
        _commandBuilder.WithPassword("StrongP@ssw0rd");
        _commandBuilder.WithConfirmPassword("DifferentP@ssw0rd");
        var request = _commandBuilder.Build();
        var result = await _sut.TestValidateAsync(request);

        result.ShouldHaveValidationErrorFor(e => e.ConfirmPassword)
            .WithErrorMessage(_messages.GetMessage(Messages.Validations.PASSWORD_MISMATCH));

        Assert.False(result.IsValid);
    }

    [Fact]
    public async Task Should_Not_Error_When_Token_Is_Valid()
    {
        var existentToken = Guid.NewGuid();
        _commandBuilder.WithToken(existentToken);
        var request = _commandBuilder.Build();

        _validationService.ExistsBy<UserToken>(Arg.Is<Expression<Func<UserToken, bool>>>(expr => Lambda.Eq(expr, e => e.Token.Equals(request.Token))))
            .Returns(true);

        var result = await _sut.TestValidateAsync(request);

        result.ShouldNotHaveValidationErrorFor(e => e.Token);
    }

    [Fact]
    public async Task Should_Pass_With_Valid_Command()
    {
        var existentToken = Guid.NewGuid();
        _commandBuilder.WithToken(existentToken);
        _commandBuilder.WithPassword("ValidP@ssw0rd123");
        _commandBuilder.WithConfirmPassword("ValidP@ssw0rd123");

        var request = _commandBuilder.Build();

        _validationService.ExistsBy<UserToken>(Arg.Is<Expression<Func<UserToken, bool>>>(expr => Lambda.Eq(expr, e => e.Token.Equals(request.Token))))
            .Returns(true);

        var result = await _sut.TestValidateAsync(request);

        Assert.True(result.IsValid);
    }
}
