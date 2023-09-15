using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Auth.Service.Domain.Resources;

public class Messages
{
    private readonly IStringLocalizer _localizer;

    public Messages(IStringLocalizerFactory factory)
    {
        var type = typeof(Messages);
        var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName!);
        _localizer = factory.Create("Messages", assemblyName.Name);
    }

    public string GetMessage(string key)
    {
        var item = _localizer[key];
        return item.ResourceNotFound ? "" : item.Value;
    }

    public string GetMessage(string key, params string[] values)
    {
        var item = _localizer[key];
        if (item.ResourceNotFound)
            return "";

        var value = item.Value;

        value = string.Format(value, values);

        return value;
    }

    public static class Entities
    {
        public const string USER = "ENTITY_USER";
        public const string USERTOKEN = "ENTITY_USERTOKEN";

    }

    public static class Validations
    {
        public const string USER_EXISTS_BY_EMAIL = "VALIDATION_USER_EXISTS_BY_EMAIL";

        public const string USERTOKEN_TOKEN_NOT_VALID = "VALIDATION_TOKEN_NOT_VALID";

        public const string NOT_EMPTY = "VALIDATION_NOT_EMPTY";
        public const string MAX_LENGTH = "VALIDATION_MAX_LENGTH";
        public const string MIN_LENGTH = "VALIDATION_MIN_LENGTH";
        public const string MUST_BE_STRONG_PASSWORD = "VALIDATION_MUST_BE_STRONG_PASSWORD";

        public const string GREATER_THAN_VALUE = "VALIDATION_GREATER_THAN_VALUE";
        public const string LESS_THAN_OR_EQUAL_VALUE = "VALIDATION_LESS_THAN_OR_EQUAL_VALUE";
        public const string INVALID_DATE = "VALIDATION_INVALID_DATE";
        public const string MUST_ONLY_CONTAIN_LETTERS = "VALIDATION_MUST_ONLY_CONTAIN_LETTERS";
        public const string INVALID_EMAIL_FORMAT = "VALIDATION_INVALID_EMAIL_FORMAT";
        public const string INVALID_PHONE_FORMAT = "VALIDATION_INVALID_PHONE_FORMAT";
        public const string INVALID_ZIP_CODE_FORMAT = "VALIDATION_INVALID_ZIP_CODE_FORMAT";


        public const string PASSWORD_MISMATCH = "VALIDATION_PASSWORD_MISMATCH";
    }

    public static class Exception
    {
        public const string AUTHORIZATION = "EXCEPTION_AUTH";
        public const string VALIDATION = "EXCEPTION_VALIDATION";
        public const string FORBIDDEN = "EXCEPTION_FORBIDDEN";
        public const string PERSISTENCE = "EXCEPTION_PERSISTENCE";
        public const string NOT_FOUND = "EXCEPTION_NOT_FOUND";
        public const string INVALID_EMAIL_OR_PASSWORD = "EXCEPTION_INVALID_EMAIL_OR_PASSWORD";
        public const string USER_NOT_ACTIVE = "EXCEPTION_USER_NOT_ACTIVE";
        public const string IF_USER_EXISTS_SEND_EMAIL = "EXCEPTION_IF_USER_EXISTS_SEND_EMAIL";
        public const string USER_ALREADY_EXISTS = "EXCEPTION_USER_ALREADY_EXISTS";

    }
}
