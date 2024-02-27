using FluentAssertions;
using GigaApp.Domain.UseCases.SignIn;
using GigaApp.Domain.UseCases.SignOn;

namespace GigaApp.Domain.Tests.SignIn;

public class SignInCommandValidatorShould
{
    private readonly SignInCommandValidator sut = new();


    [Fact]
    public void ReturnSuccess_WhenCommandIsValid()
    {
        var command = new SignInCommand("Test", "qwerty");

        var actual = sut.Validate(command);
        actual.IsValid.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(GetInvalidCommands))]
    public void ReturnFailure_WhenCommandIsInvalid(SignInCommand invalidCommand)
    {
        var actual = sut.Validate(invalidCommand);

        actual.IsValid.Should().BeFalse();
    }

    public static IEnumerable<object[]> GetInvalidCommands()
    {
        var command = new SignInCommand("Test", "qwerty");
        yield return new object[] { command with { Login = "" } };
        yield return new object[] { command with { Login = new string('*', 300) } };
        yield return new object[] { command with { Password = string.Empty } };
    }
}