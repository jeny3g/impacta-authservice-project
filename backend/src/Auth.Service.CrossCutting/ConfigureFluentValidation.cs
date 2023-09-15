using Auth.Service.Application.Users.Commands.Create;
using FluentValidation;

namespace Auth.Service.CrossCutting;

public static class ConfigureFluentValidation
{
    public static IServiceCollection InjectFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

        return services;
    }
}
