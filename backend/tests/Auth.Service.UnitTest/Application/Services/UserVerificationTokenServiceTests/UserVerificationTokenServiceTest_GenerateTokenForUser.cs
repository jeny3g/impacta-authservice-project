using Auth.Service.Application.Services;
using Auth.Service.Persistence;
using Auth.Service.UnitTest._Extensions;

namespace Auth.Service.UnitTest.Application.Services.UserVerificationTokenServiceTests;

public class UserVerificationTokenServiceTest_GenerateTokenForUser
{
    private readonly IUserVerificationTokenService _userTokenService;
    private readonly IAuthContext _context;

    public UserVerificationTokenServiceTest_GenerateTokenForUser()
    {
        EnvConfigs.ConfigureRequiredEnvs();

        _context = Substitute.For<IAuthContext>();
        _userTokenService = new UserVerificationTokenService(_context);
    }

    [Fact]
    public async Task ShouldGenerateTokenForUser()
    {
        // Setup
        var userId = Guid.NewGuid();
        var set = new List<UserToken>().AsDbSet();
        _context.UserTokens.Returns(set);

        // Action
        var token = await _userTokenService.GenerateTokenForUser(userId);

        // Assertion
        Assert.NotEqual(Guid.Empty, token);
        _context.UserTokens.Received(1).Add(Arg.Any<UserToken>());
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenDatabaseSaveFails()
    {
        var userId = Guid.NewGuid();
        var set = new List<UserToken>().AsDbSet();
        _context.UserTokens.Returns(set);

        _context.When(x => x.SaveChangesAsync(Arg.Any<CancellationToken>())).Do(x => throw new Exception());

        await Assert.ThrowsAsync<PersistenceException>(() => _userTokenService.GenerateTokenForUser(userId));
    }

    //[Fact]
    //public async Task ShouldSetCorrectExpirationForToken()
    //{
    //    var userId = Guid.NewGuid();
    //    var set = new List<UserToken>().AsDbSet();
    //    _context.UserTokens.Returns(set);

    //    var token = await _userTokenService.GenerateTokenForUser(userId);
    //    var userToken = await _context.UserTokens.FirstOrDefaultAsync(ut => ut.Token == token);

    //    var expectedExpiration = DateTime.UtcNow.AddHours(EnvConstants.TOKEN_EXPIRES_IN());
    //    Assert.True(userToken.ExpiresAt.Subtract(expectedExpiration).TotalMinutes < 1);  // Assuming < 1 min is a reasonable margin of error
    //}

    [Fact]
    public async Task ShouldReturnUserForValidToken()
    {
        var userId = Guid.NewGuid();
        var userToken = new UserToken
        {
            UserId = userId, Token = Guid.NewGuid(), User = new User()
            {
                Id = userId
            }
        };
        var set = new List<UserToken> { userToken }.AsDbSet();
        _context.UserTokens.Returns(set);

        var user = await _userTokenService.GetUserByToken(userToken.Token);

        Assert.NotNull(user);
        Assert.Equal(userId, user.Id);
    }
}