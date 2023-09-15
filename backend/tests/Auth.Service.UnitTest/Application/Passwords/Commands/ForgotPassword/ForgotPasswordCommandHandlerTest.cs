using Auth.Service.Application.Passwords.Commands.Forgot;
using Auth.Service.Mail.Interfaces;
using Auth.Service.Persistence;
using Auth.Service.UnitTest._Builders.Commands.Passwords;

namespace Auth.Service.UnitTest.Application.Passwords.Commands.ForgotPassword;

public class ForgotPasswordCommandHandlerTest
{
    readonly IAuthContext _authContext;
    readonly IUserVerificationTokenService _userTokenService;
    private readonly IFluentEmailService _emailService;


    readonly ForgotPasswordCommandHandler _sut;

    ForgotPasswordCommandBuilder _command;

    public ForgotPasswordCommandHandlerTest()
    {
        _authContext = Substitute.For<IAuthContext>();
        _userTokenService = Substitute.For<IUserVerificationTokenService>();
        _emailService = Substitute.For<IFluentEmailService>();

        _command = ForgotPasswordCommandBuilder.New(); 

        _sut = new ForgotPasswordCommandHandler(_authContext, _userTokenService, _emailService);
    }

    [Fact]
    public async Task Must_ThrowArgumentNullException_WhenRequestIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.Handle(null!, CancellationToken.None));
    }
}
