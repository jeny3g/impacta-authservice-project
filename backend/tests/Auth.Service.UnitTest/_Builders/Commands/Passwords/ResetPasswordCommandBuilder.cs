using Auth.Service.Application.Passwords.Commands.Reset;

namespace Auth.Service.UnitTest._Builders.Commands.Passwords;

public class ResetPasswordCommandBuilder : BaseBuilder<ResetPasswordCommandBuilder, ResetPasswordCommand>
{
    public Guid Token { get; private set; }
    public string Password { get; private set; }
    public string ConfirmPassword { get; private set; }


    public static new ResetPasswordCommandBuilder New()
    {
        var faker = new Faker();

        return new ResetPasswordCommandBuilder()
        {
            Token = faker.Random.Guid(),
            Password = faker.Internet.Password(),
            ConfirmPassword = faker.Internet.Password()
        };
    }

    public override ResetPasswordCommand Build()
    {
        return new ResetPasswordCommand()
        {
            Password = Password,
            ConfirmPassword = ConfirmPassword,
            Token = Token
        };
    }

    public ResetPasswordCommandBuilder WithToken(Guid token)
    {
        Token = token;
        return this;
    }

    public ResetPasswordCommandBuilder WithPassword(string password)
    {
        Password = password;
        return this;
    }

    public ResetPasswordCommandBuilder WithConfirmPassword(string confirmPassword)
    {
        ConfirmPassword = confirmPassword;
        return this;
    }
}