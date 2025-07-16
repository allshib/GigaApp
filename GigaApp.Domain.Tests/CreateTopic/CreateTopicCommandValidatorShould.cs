using FluentAssertions;
using GigaApp.Domain.UseCases.CreateTopic;

namespace GigaApp.Domain.Tests
{
    public class CreateTopicCommandValidatorShould
    {
        private readonly CreateTopicCommandValidator sut = new();


        [Fact]
        public void ReturnSuccess_WhenCommandIsValid()
        {
            var actual = sut.Validate(new CreateTopicCommand(Guid.Parse("5e69bc15-9b95-41cf-91ea-74c9a1a5c3c4"), "Some Title"));
            actual.IsValid.Should().BeTrue();
        }

        [Theory]
        [MemberData(nameof(GetInvalidCommands))]
        public void ReturnFailure_WhenCommandIsInvalid(CreateTopicCommand invalidCommand)
        {
            var actual = sut.Validate(invalidCommand);

            actual.IsValid.Should().BeFalse();
        }

        public static IEnumerable<object[]> GetInvalidCommands()
        {
            var command = new CreateTopicCommand(Guid.Empty, "Hello");
            yield return new object[] { command with {ForumId = Guid.Empty } };
            yield return new object[] { command with { Title = "" } };
            yield return new object[] { command with { Title = "                             " } };
        }
    }
}
