namespace Auth.Service.UnitTest._Configs;

public static class EnvConfigs
{
    public static void ConfigureRequiredEnvs()
    {
        Environment.SetEnvironmentVariable(EnvConstants.ENV_DATABASE_CONNECTION_STRING, "faker_conn");
        Environment.SetEnvironmentVariable(EnvConstants.ENV_APPLICATION_BASE_URL, "http://example.com");
        Environment.SetEnvironmentVariable(EnvConstants.ENV_TOKEN_EXPIRES_IN, "24");
        Environment.SetEnvironmentVariable(EnvConstants.ENV_JWT_SECRET_KEY, "aVeryLongSecretKeyThatIsAtLeastSixteenCharacters");
    }

    public static void SetEnvValue(string envName, string value)
    {
        Environment.SetEnvironmentVariable(envName, value);
    }
}
