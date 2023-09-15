namespace Auth.Service.Application.Passwords.Commands.Forgot;

public class ForgotPassword
{
    public string Email { get; set; }
}

public class ForgotPasswordCommand : IRequest<UserEmailNotRegistered>
{
    public string Email { get; set; }
}