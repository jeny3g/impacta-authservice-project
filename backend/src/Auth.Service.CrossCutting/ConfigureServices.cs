using Auth.Service.Application.Services;
using Auth.Service.Application.Services.Interfaces;
using Auth.Service.Domain.Settings;
using Auth.Service.Mail;
using Auth.Service.Mail.Interfaces;
using FluentEmail.Core.Interfaces;
using FluentEmail.SendGrid;

namespace Auth.Service.CrossCutting;

public static class ConfigureServices
{
    public static IServiceCollection InjectServices(this IServiceCollection services)
    {
        var sendgridApiKey = EnvConstants.SENDGRID_API_KEY();

        services.AddTransient<IUserVerificationTokenService, UserVerificationTokenService>();
        services.AddTransient<IPasswordHashService, PBKDF2PasswordHashService>();
        services.AddTransient<IJwtAuthenticationService, JwtAuthenticationService>();

        services.AddSingleton<ISender>(sp => new SendGridSender(sendgridApiKey));
        services.AddTransient<IFluentEmailService, FluentEmailService>();

        return services;
    }
}
