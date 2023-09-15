using Auth.Service.Application.Services;

namespace Auth.Service.UnitTest.Application.Services.PBKDF2PasswordHashServiceTests;

public class PBKDF2PasswordHashServiceTest_Hash
{
    private const int SaltSize = 16; // 128 bit 
    private const int KeySize = 32; // 256 bit
    private const int Iterations = 10000;

    private readonly PBKDF2PasswordHashService _passwordHashService;

    public PBKDF2PasswordHashServiceTest_Hash()
    {
        _passwordHashService = new PBKDF2PasswordHashService();
    }

    [Theory]
    [InlineData("password")]
    [InlineData("12345678")]
    [InlineData("Password1@")]
    public void Must_Create_Hash_In_Expected_Format(string password)
    {
        var result = _passwordHashService.HashPassword(password);

        var parts = result.Split('.', 3);
        Assert.Equal(3, parts.Length);

        Assert.Equal(Iterations, int.Parse(parts[0]));

        Assert.Equal(SaltSize, Convert.FromBase64String(parts[1]).Length);

        Assert.Equal(KeySize, Convert.FromBase64String(parts[2]).Length);
    }
}