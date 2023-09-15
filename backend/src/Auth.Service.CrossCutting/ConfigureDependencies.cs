using Auth.Service.Application.ExternalServices;
using MediatR;
using Auth.Service.Application.Services.Interfaces;
using Auth.Service.Application.Settings;
using Auth.Service.Domain.Resources;
using Auth.Service.Infra.ViaCep;

namespace Auth.Service.CrossCutting;

public static class ConfigureDependencies
{
    public static IServiceCollection InjectDependencies(this IServiceCollection services)
    {
        services.InjectApplicationDependencies();
        services.InjectInfraDependencies();

        return services;
    }

    private static IServiceCollection InjectApplicationDependencies(this IServiceCollection services)
    {
        services.AddSingleton<Messages>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        services.AddScoped<IBaseValidationService, BaseValidationService>();

        return services;
    }

    private static IServiceCollection InjectInfraDependencies(this IServiceCollection services)
    {
        services
            .AddHttpClient<IViaCEPClient, ViaCepClient>()
            .SetHandlerLifetime(TimeSpan.FromMinutes(5));

        return services;
    }
}
