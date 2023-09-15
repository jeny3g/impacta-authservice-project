using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Auth.Service.Application.Services;
using Microsoft.IdentityModel.Tokens;

public class JwtAuthenticationServiceTest_GenerateJWT
{
    private readonly JwtAuthenticationService _sut;

    public JwtAuthenticationServiceTest_GenerateJWT()
    {
        EnvConfigs.ConfigureRequiredEnvs();

        _sut = new JwtAuthenticationService();
    }

    [Fact]
    public void GenerateJWT_ValidUser_ReturnsValidToken()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = "user@example.com",
            Name = "User"
        };

        // Act
        var jwt = _sut.GenerateJWT(user);

        // Assert
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(EnvConstants.JWT_SECRET_KEY());
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        tokenHandler.ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);
        var token = validatedToken as JwtSecurityToken;

        var idClaim = token.Claims.First(c => c.Type == "nameid").Value;
        var nameClaim = token.Claims.First(c => c.Type == "unique_name").Value;
        var emailClaim = token.Claims.First(c => c.Type == "email").Value; 

        Assert.Equal(user.Id.ToString(), idClaim);
        Assert.Equal(user.Name, nameClaim);
        Assert.Equal(user.Email, emailClaim);
    }
}