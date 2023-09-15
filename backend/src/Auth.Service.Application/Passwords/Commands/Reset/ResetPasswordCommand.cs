namespace Auth.Service.Application.Passwords.Commands.Reset;

public class ResetPassword
{
    public Guid Token { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }
}

public class ResetPasswordCommand : IRequest<CreateSuccess>
{
    public Guid Token { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }
}