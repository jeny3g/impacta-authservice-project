using Auth.Service.Application.Services;
using Auth.Service.Persistence;

namespace Auth.Service.UnitTest.Application.Services.UserVerificationTokenServiceTests;

public class UserVerificationTokenServiceTest_GetUserByToken
{
    private readonly IUserVerificationTokenService _userTokenService;
    private readonly IAuthContext _context;

    public UserVerificationTokenServiceTest_GetUserByToken()
    {
        EnvConfigs.ConfigureRequiredEnvs();

        _context = Substitute.For<IAuthContext>();
        _userTokenService = new UserVerificationTokenService(_context);
    }

}