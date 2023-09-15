using Auth.Service.Application.Sessions.Commands.Authenticate;

namespace Auth.Service.UnitTest._Builders.Commands.Sessions;

public class AuthenticateSessionCommandBuilder
{
    private string _email;
    private string _password;

    public static AuthenticateSessionCommandBuilder New()
    {
        return new AuthenticateSessionCommandBuilder();
    }

    public AuthenticateSessionCommandBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }

    public AuthenticateSessionCommandBuilder WithPassword(string password)
    {
        _password = password;
        return this;
    }

    public AuthenticateSessionCommand Build()
    {
        return new AuthenticateSessionCommand
        {
            Email = _email,
            Password = _password
        };
    }
}

