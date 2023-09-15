namespace Auth.Service.Domain.Exceptions;

public class MissingClaimException : Exception
{
    public string MissingClaimType { get; }

    public MissingClaimException(string claimType)
        : base($"The expected claim '{claimType}' is missing.")
    {
        MissingClaimType = claimType;
    }

    public MissingClaimException(string claimType, string message)
        : base(message)
    {
        MissingClaimType = claimType;
    }

    public MissingClaimException(string claimType, string message, Exception innerException)
        : base(message, innerException)
    {
        MissingClaimType = claimType;
    }
}
