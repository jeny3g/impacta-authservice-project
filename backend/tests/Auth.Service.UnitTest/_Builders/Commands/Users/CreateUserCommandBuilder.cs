using Auth.Service.Application.Users.Commands.Create;

namespace Auth.Service.UnitTest._Builders.Commands.Users;

public class CreateUserCommandBuilder : BaseBuilder<CreateUserCommandBuilder, CreateUserCommand>
{
    private string Name;
    private string Email;
    private string Password;

    public static new CreateUserCommandBuilder New()
    {
        var faker = new Faker();

        return new CreateUserCommandBuilder()
        {
            Name = faker.Person.FullName,
            Email = faker.Person.Email,
            Password = faker.Internet.Password()
        };
    }

    public override CreateUserCommand Build()
    {
        return new CreateUserCommand()
        {
            Name = Name,
            Email = Email,
            Password = Password
        };
    }

    public CreateUserCommandBuilder WithName(string name)
    {
        Name = name;
        return this;
    }

    public CreateUserCommandBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }

    public CreateUserCommandBuilder WithPassword(string password)
    {
        Password = password;
        return this;
    }
}