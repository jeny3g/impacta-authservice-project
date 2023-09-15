namespace Auth.Service.UnitTest.Domain.Utils.UtilsTests;

public class UtilsTest_HasValidPassword
{
    [Theory]
    [InlineData("Password1@")]
    [InlineData("Abcdef1@")]
    [InlineData("Test$123A")]
    public void Must_Return_True_When_Password_Is_Valid(string password)
    {
        var result = password.HasValidPassword();

        Assert.True(result);
    }

    [Theory]
    [InlineData("password")]
    [InlineData("PASSWORD")]
    [InlineData("12345678")]
    [InlineData("@@@@@@@@")]
    [InlineData("Password")]
    [InlineData("password1")]
    [InlineData("PASSWORD1")]
    [InlineData("Password1")]
    [InlineData("password@")]
    [InlineData("PASSWORD@")]
    [InlineData(null)]
    public void Must_Return_False_When_Password_Is_Invalid(string password)
    {
        var result = password.HasValidPassword();

        Assert.False(result);
    }
}