using Auth.Service.Application.Passwords.Commands.Forgot;

namespace Auth.Service.UnitTest._Builders.Commands.Passwords;

public class ForgotPasswordCommandBuilder : BaseBuilder<ForgotPasswordCommandBuilder, ForgotPasswordCommand>
{
    private string Email;

    public static new ForgotPasswordCommandBuilder New()
    {
        var faker = new Faker();

        return new ForgotPasswordCommandBuilder()
        {
            Email = faker.Person.Email,
        };
    }

    public override ForgotPasswordCommand Build()
    {
        return new ForgotPasswordCommand()
        {
            Email = Email,
        };
    }

    public ForgotPasswordCommandBuilder WithEmail(string email)
    {
        Email = email;
        return this;
    }
}