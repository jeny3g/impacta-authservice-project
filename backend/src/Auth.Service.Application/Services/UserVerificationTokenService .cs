using Auth.Service.Domain.Settings;

namespace Auth.Service.Application.Services;

public class UserVerificationTokenService : IUserVerificationTokenService 
{
    private readonly IAuthContext _context;

    public UserVerificationTokenService (IAuthContext context)
    {
        _context = context;
    }

    public async Task<Guid> GenerateTokenForUser(Guid userId)
    {
        var userToken = new UserToken
        {
            UserId = userId,
            Token = Guid.NewGuid(),
            ExpiresAt = DateTime.UtcNow.AddHours(EnvConstants.TOKEN_EXPIRES_IN())
        };

        try
        {
            _context.UserTokens.Add(userToken);
            await _context.SaveChangesAsync();

            return userToken.Token;
        }
        catch (Exception ex)
        {
            throw new PersistenceException(ex);
        }
    }

    public async Task<User> GetUserByToken(Guid token)
    {
        var userToken = await _context.UserTokens
            .FirstOrDefaultAsync(t => t.Token.Equals(token));

        if (userToken.User is null)
            throw new NotFoundException("User not found");

        return userToken.User;
    }

    public async Task<UserToken> GetUserTokenByToken(Guid token)
    {
        var userToken = await _context.UserTokens
            .FirstOrDefaultAsync(t => t.Token.Equals(token));

        return userToken;
    }

    public string GetVerificationUrl(Guid token)
    {
        return $"{EnvConstants.APPLICATION_BASE_URL()}/verify?token={token}";
    }

    public string GetResetPasswordUrl(Guid token)
    {
        return $"{EnvConstants.APPLICATION_BASE_URL()}/reset-password?token={token}";
    }
}