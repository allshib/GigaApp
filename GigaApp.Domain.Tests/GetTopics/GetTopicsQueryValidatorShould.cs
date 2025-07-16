using FluentAssertions;
using GigaApp.Domain.UseCases.GetTopics;

namespace GigaApp.Domain.Tests.GetTopics
{
    public class GetTopicsQueryValidatorShould
    {
        private readonly GetTopicsQueryValidator sut = new();


        [Fact]
        public void ReturnsSuccess_WhenQueryIsValid()
        {
            var query = new GetTopicsQuery(
                Guid.Parse("76ad4a33-2b03-4716-9663-b79c7f3acc07"), 10, 5);

            sut.Validate(query).IsValid.Should().BeTrue();
        }


        public static IEnumerable<object[]> GetInvalidQuery()
        {
            var query = new GetTopicsQuery(
               Guid.Parse("76ad4a33-2b03-4716-9663-b79c7f3acc07"), 10, 5);

            yield return new object[] { query with { ForumId = Guid.Empty } };
            yield return new object[] { query with { Skip = -40 } };
            yield return new object[] { query with { Take = -40 } };
        }

        [Theory]
        [MemberData(nameof(GetInvalidQuery))]
        public void ReturnFailure_WhenQueryIsInvalid(GetTopicsQuery query)
        {

            sut.Validate(query).IsValid.Should().BeFalse();
        }

    }
}
