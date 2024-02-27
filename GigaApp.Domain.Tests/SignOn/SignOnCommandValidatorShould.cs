using FluentAssertions;
using GigaApp.Domain.UseCases.CreateTopic;
using GigaApp.Domain.UseCases.SignOn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigaApp.Domain.UseCases.SignOn;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GigaApp.Domain.Tests.SignOn
{
    public class SignOnCommandValidatorShould
    {
        private readonly SignOnCommandValidator sut = new();

        public SignOnCommandValidatorShould()
        {
            
        }
        [Fact]
        public void ReturnSuccess_WhenCommandIsValid()
        {
            var command = new SignOnCommand("Test", "qwerty");

            var actual = sut.Validate(command);
            actual.IsValid.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(GetInvalidCommands))]
        public void ReturnFailure_WhenCommandIsInvalid(SignOnCommand invalidCommand)
        {
            var actual = sut.Validate(invalidCommand);

            actual.IsValid.Should().BeFalse();
        }

        public static IEnumerable<object[]> GetInvalidCommands()
        {
            var command = new SignOnCommand("Test", "qwerty");
            yield return new object[] { command with { Login = "" } };
            yield return new object[] { command with { Login = new string('*', 300) } };
            yield return new object[] { command with { Password = string.Empty } };
        }

    }
}
