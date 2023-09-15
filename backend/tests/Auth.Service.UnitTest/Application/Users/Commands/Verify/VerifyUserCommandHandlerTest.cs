using Auth.Service.Application.Models.ViewModels;
using Auth.Service.Application.Users.Commands.Verify;
using Auth.Service.Persistence;
using Auth.Service.UnitTest._Builders.Commands.Users;
using NSubstitute.ExceptionExtensions;

namespace Auth.Service.UnitTest.Application.Users.Commands.Verify;

public class VerifyUserCommandHandlerTest
{
    readonly IAuthContext _authContext;
    readonly IUserVerificationTokenService _userTokenService;
    readonly Messages _messages;

    readonly VerifyUserCommandHandler _sut;

    VerifyUserCommandBuilder _command;

    public VerifyUserCommandHandlerTest()
    {
        _authContext = Substitute.For<IAuthContext>();
        _userTokenService = Substitute.For<IUserVerificationTokenService>();
        _messages = MessagesConfig.Build();

        _command = VerifyUserCommandBuilder.New();

        _sut = new VerifyUserCommandHandler(_authContext, _messages, _userTokenService);
    }

    [Fact]
    public async Task Must_ThrowArgumentNullException_WhenRequestIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.Handle(null!, CancellationToken.None));
    }

    [Fact]
    public async Task Must_ThrowNotFoundException_WhenUserTokenHasNoUser()
    {
        var request = _command.Build();
        _userTokenService.GetUserTokenByToken(request.Token).Returns(new UserToken { User = null });

        await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Must_ThrowNotFoundException_WhenTokenIsExpired()
    {
        var request = _command.Build();
        _userTokenService.GetUserTokenByToken(request.Token).Returns(new UserToken { User = new User(), ExpiresAt = DateTime.UtcNow.AddMinutes(-10) });

        await Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(request, CancellationToken.None));
    }

    [Fact]
    public async Task Must_UpdateUserTokenAndUser_WhenTokenIsValid()
    {
        var request = _command.Build();
        _userTokenService.GetUserTokenByToken(request.Token).Returns(new UserToken { User = new User(), ExpiresAt = DateTime.UtcNow.AddMinutes(10) });

        await _sut.Handle(request, CancellationToken.None);

        _authContext.UserTokens.Received(1).Update(Arg.Any<UserToken>());
        _authContext.Users.Received(1).Update(Arg.Any<User>());
    }

    [Fact]
    public async Task Must_SaveChangesAsync_WhenTokenIsValid()
    {
        var request = _command.Build();
        _userTokenService.GetUserTokenByToken(request.Token).Returns(new UserToken { User = new User(), ExpiresAt = DateTime.UtcNow.AddMinutes(10) });

        await _sut.Handle(request, CancellationToken.None);

        await _authContext.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Must_ReturnCreateSuccess_WhenRequestIsValid()
    {
        var request = _command.Build();
        var dummyUserId = Guid.NewGuid();
        _userTokenService.GetUserTokenByToken(request.Token).Returns(new UserToken { User = new User { Id = dummyUserId }, ExpiresAt = DateTime.UtcNow.AddMinutes(10) });

        var result = await _sut.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.IsType<CreateSuccess>(result);
        Assert.Equal(dummyUserId, result.Id);  // Assuming CreateSuccess has a UserId property
    }

    [Fact]
    public async Task Must_ThrowPersistenceException_WhenExceptionOccurs()
    {
        var request = _command.Build();
        _userTokenService.GetUserTokenByToken(request.Token).Returns(new UserToken { User = new User(), ExpiresAt = DateTime.UtcNow.AddMinutes(10) });
        _authContext.SaveChangesAsync(Arg.Any<CancellationToken>()).Throws(new Exception());

        await Assert.ThrowsAsync<PersistenceException>(() => _sut.Handle(request, CancellationToken.None));
    }
}
