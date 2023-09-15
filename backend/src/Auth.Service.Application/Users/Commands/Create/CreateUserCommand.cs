namespace Auth.Service.Application.Users.Commands.Create;

public class CreateUser
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}

public class CreateUserCommand : IRequest<CreateSuccess>
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}