using Auth.Service.Application.Users.Commands.Verify;

namespace Auth.Service.UnitTest._Builders.Commands.Users;

public class VerifyUserCommandBuilder : BaseBuilder<VerifyUserCommandBuilder, VerifyUserCommand>
{
    public Guid Token { get; private set; }

    public static new VerifyUserCommandBuilder New()
    {
        var faker = new Faker();

        return new VerifyUserCommandBuilder()
        {
            Token = faker.Random.Guid(),
        };
    }

    public override VerifyUserCommand Build()
    {
        return new VerifyUserCommand()
        {
            Token = Token
        };
    }

    public VerifyUserCommandBuilder WithToken(Guid token)
    {
        Token = token;
        return this;
    }
}