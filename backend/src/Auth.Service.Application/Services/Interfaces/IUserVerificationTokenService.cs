namespace Auth.Service.Application.Services.Interfaces;

public interface IUserVerificationTokenService 
{
    Task<Guid> GenerateTokenForUser(Guid userId);

    Task<User> GetUserByToken(Guid token);

    Task<UserToken> GetUserTokenByToken(Guid token);

    string GetVerificationUrl(Guid token);

    string GetResetPasswordUrl(Guid token);
}