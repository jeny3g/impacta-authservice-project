namespace Auth.Service.Application.Services.Interfaces;

public interface IJwtAuthenticationService
{
    string GenerateJWT(User user);
}
