using Auth.Service.Application.Sessions.Commands.Authenticate;
using Auth.Service.Persistence;
using Auth.Service.UnitTest._Builders.Commands.Sessions;

namespace Auth.Service.UnitTest.Application.Sessions.Commands.Authenticate;

public class AuthenticateSessionCommandHandlerTest
{
    readonly IAuthContext _authContext;
    readonly IPasswordHashService _passwordHashService;
    readonly IJwtAuthenticationService _jwtAuthenticationService;
    readonly Messages _messages;

    readonly AuthenticateSessionCommandHandler _sut;

    AuthenticateSessionCommandBuilder _command;

    public AuthenticateSessionCommandHandlerTest()
    {
        _authContext = Substitute.For<IAuthContext>();
        _passwordHashService = Substitute.For<IPasswordHashService>();
        _jwtAuthenticationService = Substitute.For<IJwtAuthenticationService>();
        _messages = MessagesConfig.Build();

        _command = AuthenticateSessionCommandBuilder.New(); // Assuming a similar builder

        _sut = new AuthenticateSessionCommandHandler(_authContext, _messages, _passwordHashService, _jwtAuthenticationService);
    }

    [Fact]
    public async Task Must_ThrowArgumentNullException_WhenRequestIsNull()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() => _sut.Handle(null!, CancellationToken.None));
    }
}
