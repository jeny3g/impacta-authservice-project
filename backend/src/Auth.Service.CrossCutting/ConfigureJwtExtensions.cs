using System.Text;
using Auth.Service.Domain.Settings;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Auth.Service.CrossCutting;

public static class ConfigureJwtExtensions
{
    public static IServiceCollection InjectJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EnvConstants.JWT_SECRET_KEY())),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

        return services;
    }
}



