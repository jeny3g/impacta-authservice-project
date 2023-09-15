namespace Auth.Service.UnitTest.Domain.Utils.UtilsTests;

public class UtilsTest_IsValidName
{
    [Theory]
    [InlineData("José")]
    [InlineData("John")]
    [InlineData("Anne-Marie")]
    [InlineData("O'Connor")]
    [InlineData("Van der Waals")]
    [InlineData("lettersOnly")]
    [InlineData("LETTERSONLY")]
    [InlineData("MixedLetters")]
    public void Must_Return_True_When_Input_Is_Valid_Name(string value)
    {
        var result = value.IsValidName();

        Assert.True(result);
    }

    [Theory]
    [InlineData("letters123")]
    [InlineData("1234")]
    [InlineData("letters_with_underscores")]
    [InlineData("!@#$%")]
    [InlineData("Johh!Doe")]
    public void Must_Return_False_When_Input_Is_Not_Valid_Name(string value)
    {
        var result = value.IsValidName();

        Assert.False(result);
    }

    [Theory]
    [InlineData(null)]
    public void Must_Return_False_When_Input_Is_Null(string value)
    {
        var result = value.IsValidName();

        Assert.False(result);
    }

    [Theory]
    [InlineData("John123")]
    public void Given_A_String_With_Numbers_Must_Return_False(string value)
    {
        var result = value.IsValidName();

        Assert.False(result);
    }

    [Theory]
    [InlineData("Johh Doe")]
    public void Given_A_Valid_Name_With_Space_Must_Return_True(string value)
    {
        var result = value.IsValidName();

        Assert.True(result);
    }
}