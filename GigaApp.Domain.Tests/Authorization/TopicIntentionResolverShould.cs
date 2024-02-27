using FluentAssertions;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.Identity;
using GigaApp.Domain.UseCases.CreateTopic;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Domain.Tests.Authorization
{
    public class TopicIntentionResolverShould
    {
        private readonly TopicIntetntionResolver sut;

        public TopicIntentionResolverShould()
        {
            sut = new TopicIntetntionResolver();
        }


        [Fact]
        public void ReturnFalse_WhenIntetntionNotInEnum()
        {
            var intention = (TopicIntention) (- 1);
            sut.IsAllowed(new Mock<IIdentity>().Object, intention).Should().BeFalse();
        }


        [Fact]
        public void ReturnFalse_WhenCheckingTopicCreateIntention_AndUserIsGuest()
        {
            sut.IsAllowed(User.Guest, TopicIntention.Create).Should().BeFalse();
        }

        [Fact]
        public void ReturnTrue_WhenCheckingTopicCreateIntention_AndUserIsAuthenticated()
        {
            sut.IsAllowed(
                new User(Guid.Parse("747d3fd5-0241-4b00-a970-6d4e9b3f15e1")),
                TopicIntention.Create)
                .Should().BeTrue();
        }
    }
}
