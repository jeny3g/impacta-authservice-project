using System.Diagnostics.CodeAnalysis;
using Auth.Service.Application.Users.Commands.Create;

[assembly: ExcludeFromCodeCoverage]
namespace Auth.Service.CrossCutting;

public static class ConfigureMediatR
{
    public static IServiceCollection InjectMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>());

        return services;
    }
}
