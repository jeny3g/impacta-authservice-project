using System.Reflection;
using System.Runtime.CompilerServices;
using Auth.Service.Domain.Exceptions;

[assembly: InternalsVisibleTo("Auth.Service.UnitTest")]
namespace Auth.Service.Domain.Settings;

public static class EnvConstants
{
    internal const string ENV_DATABASE_CONNECTION_STRING = "DATABASE_CONNECTION_STRING";
    internal const string ENV_APPLICATION_BASE_URL = "APPLICATION_BASE_URL";

    internal const string ENV_TOKEN_EXPIRES_IN = "TOKEN_EXPIRES_IN";

    internal const string ENV_JWT_SECRET_KEY = "JWT_SECRET_KEY";

    internal const string ENV_SENDGRID_API_KEY = "SENDGRID_API_KEY";

    internal const string ENV_VIACEP_BASE_URL = "VIACEP_BASE_URL";



    private static BusinessException InvalidValue(string envName) => new($"Invalid value for env var {envName}");

    private static string GetEnvironmentVariable(string name)
    {
        return Environment.GetEnvironmentVariable(name);
    }

    private static string GetEnvironmentVariable(string name, string defaultValue, bool errorIfEmpty = false)
    {
        var value = GetEnvironmentVariable(name);

        if (string.IsNullOrWhiteSpace(value))
        {
            if (errorIfEmpty)
            {
                throw new Exception($"Env var {name} must contain value.");
            }
            return defaultValue;
        }

        return value;
    }

    private static string GetRequiredEnvironmentVariable(string name)
    {
        return GetEnvironmentVariable(name, null, true) ?? string.Empty;
    }

    public static int TOKEN_EXPIRES_IN()
    {
        var defaultValue = 24;

        var value = GetEnvironmentVariable(ENV_TOKEN_EXPIRES_IN, defaultValue.ToString());

        if (int.TryParse(value, out var result))
            return result; 
        
        throw InvalidValue(ENV_TOKEN_EXPIRES_IN);
    }

    public static string APPLICATION_BASE_URL() => GetRequiredEnvironmentVariable(ENV_APPLICATION_BASE_URL);

    public static string JWT_SECRET_KEY() => GetEnvironmentVariable(ENV_JWT_SECRET_KEY, "aVeryLongSecretKeyThatIsAtLeastSixteenCharacters");

    public static string SENDGRID_API_KEY() => GetRequiredEnvironmentVariable(ENV_SENDGRID_API_KEY);
    public static string VIACEP_BASE_URL() => GetEnvironmentVariable(ENV_VIACEP_BASE_URL);

    public static string DATABASE_CONNECTION_STRING() => GetRequiredEnvironmentVariable(ENV_DATABASE_CONNECTION_STRING);

    public static void ValidateRequiredEnvs()
    {
        var methodInfos = typeof(EnvConstants)
            .GetMethods(BindingFlags.Public | BindingFlags.Static)
            .Where(e => e.ReturnType != typeof(void))
            .Where(e => !e.GetParameters().Any())
            .ToList();

        var errors = new List<string>();

        foreach (var item in methodInfos)
        {
            try
            {
                item.Invoke(null, null);
            }
            catch (Exception ex)
            {
                errors.Add(ex.InnerException?.Message ?? ex.Message);
            }
        }

        if (errors.Count > 0) throw new Exception(string.Join("\n", errors));
    }
}
