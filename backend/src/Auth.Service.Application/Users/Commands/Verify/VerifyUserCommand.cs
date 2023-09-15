namespace Auth.Service.Application.Users.Commands.Verify;

public class VerifyUser
{
    public Guid Token { get; set; }
}

public class VerifyUserCommand : IRequest<CreateSuccess>
{
    public Guid Token { get; set; }
}