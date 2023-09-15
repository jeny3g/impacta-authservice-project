namespace Auth.Service.UnitTest.Domain.Utils.UtilsTests;

public class UtilsTest_IsValidEmail
{
    [Theory]
    [InlineData("test@test.com")]
    [InlineData("test.test@test.com")]
    [InlineData("test.test.test@test.com")]
    [InlineData("test.test@test.co.uk")]
    public void Must_Return_True_When_Email_Is_Valid(string email)
    {
        var result = email.IsValidEmail();

        Assert.True(result);
    }

    [Theory]
    [InlineData("test")]
    [InlineData("test@")]
    [InlineData("test@test")]
    [InlineData("@test.com")]
    [InlineData("test.@test.com")]
    [InlineData(".test@test.com")]
    [InlineData("test@test.com.")]
    [InlineData("test@.com")]
    [InlineData("test@test..com")]
    [InlineData(null)]
    [InlineData("")]
    public void Must_Return_False_When_Email_Is_Invalid(string email)
    {
        var result = email.IsValidEmail();

        Assert.False(result);
    }
}