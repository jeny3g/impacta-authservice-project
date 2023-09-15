using Auth.Service.Persistence;
using Auth.Service.Application.Passwords.Commands.Reset;
using Auth.Service.UnitTest._Builders.Commands.Passwords;
using NSubstitute.ExceptionExtensions;
using Microsoft.EntityFrameworkCore;

namespace Auth.Service.UnitTest.Application.Passwords.Commands.Reset;

public class ResetPasswordCommandHandlerTest
{
    readonly IAuthContext _authContext;
    readonly Messages _messages;
    readonly IPasswordHashService _passwordHashService;
    readonly IUserVerificationTokenService  _userTokenService;

    readonly ResetPasswordCommandHandler _sut;

    ResetPasswordCommandBuilder _builder;

    public ResetPasswordCommandHandlerTest()
    {
        _authContext = Substitute.For<IAuthContext>();
        _messages = MessagesConfig.Build();
        _passwordHashService = Substitute.For<IPasswordHashService>();
        _userTokenService = Substitute.For<IUserVerificationTokenService >();

        _builder = ResetPasswordCommandBuilder.New();

        _sut = new ResetPasswordCommandHandler(_authContext, _messages, _passwordHashService, _userTokenService);
    }

    [Fact]
    public async Task Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.Handle(null, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldThrowNotFoundException_WhenUserNotFound()
    {
        var request = _builder.Build();

        _userTokenService.GetUserByToken(request.Token).Returns((User)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ShouldResetPasswordSuccessfully_WhenValidTokenAndRequestProvided()
    {
        var request = _builder.Build();
        var dummyUser = new User { Id = Guid.NewGuid() };

        _userTokenService.GetUserByToken(request.Token).Returns(dummyUser);
        _passwordHashService.HashPassword(request.Password).Returns("hashed_password");

        var result = await _sut.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(dummyUser.Id, result.Id);
        Assert.True(dummyUser.Active);
        Assert.Equal("hashed_password", dummyUser.PasswordHash);
    }

    [Fact]
    public async Task Handle_ShouldThrowPersistenceException_WhenDbUpdateExceptionOccurs()
    {
        var request = _builder.Build();
        var dummyUser = new User { Id = Guid.NewGuid() };

        _userTokenService.GetUserByToken(request.Token).Returns(dummyUser);

        _authContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Throws(new DbUpdateException());

        await Assert.ThrowsAsync<PersistenceException>(() => _sut.Handle(request, CancellationToken.None));
    }
}