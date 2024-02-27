using FluentAssertions;
using FluentValidation;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.UseCases.CreateForum;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Tests.CreateForum
{
    public class CreateForumCommandValidatorShould
    {
        private readonly CreateForumCommandValidator sut = new();


        [Fact]
        public void ReturnSuccess_WhenCommandIsValid()
        {
            var command = new CreateForumCommand("Forum");

            sut.Validate(command).IsValid.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(GetInvalidCommands))]
        public void ReturnFailure_WhenCommandIsInvalid(CreateForumCommand command)
        {
            sut.Validate(command).IsValid.Should().BeFalse();
        }

        public static IEnumerable<object[]> GetInvalidCommands()
        {
            var validCommand = new CreateForumCommand("Title");

            yield return new object[] { validCommand with { Title = string.Empty } };
            yield return new object[] { validCommand with { Title = new string('*', 500) } };
        }
    }
}
