using Microsoft.EntityFrameworkCore;
using Auth.Service.Domain.Settings;
using Auth.Service.Persistence;

namespace Auth.Service.CrossCutting;
public static class ConfigureDatabase
{
    public static IServiceCollection InjectDatabases(this IServiceCollection services)
    {
        var connString = EnvConstants.DATABASE_CONNECTION_STRING();

        services.AddDbContext<AuthContext>(options =>
        {
            options
                .UseLazyLoadingProxies()
                .UseNpgsql(connString);
        });

        services.AddScoped<IAuthContext>(provider => provider.GetService<AuthContext>());

        return services;
    }
}
