using Auth.Service.Api.Middlewares;

namespace Auth.Service.Api.Helpers;

public static class MiddlewaresHelper
{
    public static IApplicationBuilder UseLoggerScope(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoggerScopeMiddleware>();
    }
}
