using Auth.Service.Application.Sessions.Models;

namespace Auth.Service.Application.Sessions.Commands.Authenticate;

public class AuthenticateSession
{
    public string Email { get; set; }

    public string Password { get; set; }
}

public class AuthenticateSessionCommand : IRequest<AuthenticateSessionViewModel>
{
    public string Email { get; set; }

    public string Password { get; set; }
}