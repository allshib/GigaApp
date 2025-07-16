using FluentAssertions;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.Exceptions;
using GigaApp.Domain.Identity;
using GigaApp.Domain.UseCases.CreateForum;
using Moq;
using System.Net;

namespace GigaApp.Domain.Tests.Authorization
{
    public class IntentionManagerShould
    {

        public IntentionManagerShould()
        {
            
        }

        [Fact]
        public void ReturnFalse_WhenNoMatchingResolver()
        {
            var intentionProvider = new Mock<IIdentityProvider>();
            var resolvers = new IIntentionResolver[]
            {
                new Mock<IIntentionResolver<DomainErrorCode>>().Object,
                new Mock<IIntentionResolver<HttpStatusCode>>().Object,
            };

            var sut = new IntentionManager(resolvers, intentionProvider.Object);

            sut.IsAllowed(ForumIntention.Create).Should().BeFalse();
        }

        [Theory]
        [InlineData(true, true)]
        [InlineData(false, false)]
        public void ReturnMatchingResolverResult(bool expectedResult, bool expected)
        {
            var intentionProvider = new Mock<IIdentityProvider>();
            intentionProvider
                .Setup(x => x.Current)
                .Returns(new User(Guid.Parse("caf70cb2-2268-44cc-932a-2d35112387f5"), Guid.Empty));

            var resolver = new Mock<IIntentionResolver<ForumIntention>>();
            resolver
                .Setup(r=>r.IsAllowed(It.IsAny<IIdentity>(), It.IsAny<ForumIntention>()))
                .Returns(expectedResult);
            
           

            var sut = new IntentionManager(new IIntentionResolver[] { resolver.Object }, intentionProvider.Object);

            sut.IsAllowed(ForumIntention.Create).Should().Be(expected);
        }

    }
}
