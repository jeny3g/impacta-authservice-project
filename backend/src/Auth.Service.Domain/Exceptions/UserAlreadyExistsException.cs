namespace Auth.Service.Domain.Exceptions;

public class UserAlreadyExistsException : Exception
{
    public bool IsDefaultMessage { get; private set; }

    public UserAlreadyExistsException() : base()
    {
        IsDefaultMessage = true;
    }

    public UserAlreadyExistsException(string message) : base(message) { }
}
