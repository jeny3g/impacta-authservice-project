namespace Auth.Service.Domain.Exceptions;

public class UserEmailNotRegistered : Exception
{
    public bool IsDefaultMessage { get; private set; }

    public UserEmailNotRegistered() : base()
    {
        IsDefaultMessage = true;
    }

    public UserEmailNotRegistered(string message) : base(message) { }
}
