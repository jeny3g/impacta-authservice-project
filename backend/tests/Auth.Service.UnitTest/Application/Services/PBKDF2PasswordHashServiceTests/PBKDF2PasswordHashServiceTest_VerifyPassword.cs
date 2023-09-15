using Auth.Service.Application.Services;

namespace Auth.Service.UnitTest.Application.Services.PBKDF2PasswordHashServiceTests;

public class PBKDF2PasswordHashServiceTest_VerifyPassword
{
    private readonly PBKDF2PasswordHashService _passwordHashService;

    public PBKDF2PasswordHashServiceTest_VerifyPassword()
    {
        _passwordHashService = new PBKDF2PasswordHashService();
    }

    [Theory]
    [InlineData("password")]
    [InlineData("12345678")]
    [InlineData("Password1@")]
    public void Must_Return_True_For_Correct_Password(string password)
    {
        var hashedPassword = _passwordHashService.HashPassword(password);

        var result = _passwordHashService.VerifyPassword(hashedPassword, password);

        Assert.True(result);
    }

    [Theory]
    [InlineData("password", "wrongPassword")]
    [InlineData("12345678", "87654321")]
    [InlineData("Password1@", "Password2@")]
    public void Must_Return_False_For_Wrong_Password(string password, string wrongPassword)
    {
        var hashedPassword = _passwordHashService.HashPassword(password);

        var result = _passwordHashService.VerifyPassword(hashedPassword, wrongPassword);

        Assert.False(result);
    }
}