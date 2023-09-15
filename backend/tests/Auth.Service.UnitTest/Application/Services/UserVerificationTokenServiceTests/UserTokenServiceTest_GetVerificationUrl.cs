using Auth.Service.Persistence;

namespace Auth.Service.UnitTest.Application.Services.UserVerificationTokenServiceTests;

public class UserVerificationTokenServiceTest_GetVerificationUrl
{
    private readonly IUserVerificationTokenService _userTokenService;
    private readonly IAuthContext _context;

    public UserVerificationTokenServiceTest_GetVerificationUrl()
    {
        EnvConfigs.ConfigureRequiredEnvs();

        _context = Substitute.For<IAuthContext>();
        _userTokenService = new Service.Application.Services.UserVerificationTokenService(_context);
    }

    [Fact]
    public void ShouldReturnCorrectVerificationUrl()
    {
        var token = Guid.NewGuid();
        var expectedUrl = $"{EnvConstants.APPLICATION_BASE_URL()}/verify?token={token}";

        var actualUrl = _userTokenService.GetVerificationUrl(token);

        Assert.Equal(expectedUrl, actualUrl);
    }
}