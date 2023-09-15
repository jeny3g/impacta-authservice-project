using Auth.Service.Application.Users.Commands.Verify;
using Neleus.LambdaCompare;
using System.Linq.Expressions;
using Auth.Service.UnitTest._Builders.Commands.Users;

namespace Auth.Service.UnitTest.Application.Users.Commands.Verify;

public class VerifyUserCommandValidatorTest
{
    private readonly Messages _messages;
    private readonly IBaseValidationService _validationService;
    private readonly VerifyUserCommandBuilder _commandBuilder;
    private readonly Faker _faker;
    private readonly VerifyUserCommandValidator _sut;

    public VerifyUserCommandValidatorTest()
    {
        _messages = MessagesConfig.Build();
        _validationService = Substitute.For<IBaseValidationService>();
        _commandBuilder = VerifyUserCommandBuilder.New();
        _faker = new Faker();
        _sut = new VerifyUserCommandValidator(_messages, _validationService);
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

    [Fact]
    public async Task Should_Pass_With_Valid_Token()
    {
        var existentToken = Guid.NewGuid();
        _commandBuilder.WithToken(existentToken);

        var request = _commandBuilder.Build();

        _validationService.ExistsBy<UserToken>(Arg.Is<Expression<Func<UserToken, bool>>>(expr => Lambda.Eq(expr, e => e.Token.Equals(request.Token))))
            .Returns(true);

        var result = await _sut.TestValidateAsync(request);

        result.ShouldNotHaveValidationErrorFor(e => e.Token);
    }
}
