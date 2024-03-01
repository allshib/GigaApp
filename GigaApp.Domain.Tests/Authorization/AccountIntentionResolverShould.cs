using FluentAssertions;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.Identity;
using GigaApp.Domain.UseCases.CreateForum;
using GigaApp.Domain.UseCases.SignOut;
using Moq;

namespace GigaApp.Domain.Tests.Authorization;

public class AccountIntentionResolverShould
{
    private readonly AccountIntentionResolver sut;

    public AccountIntentionResolverShould()
    {
        sut = new AccountIntentionResolver();
    }


    [Fact]
    public void ReturnFalse_WhenIntetntionNotInEnum()
    {
        var intention = (AccountIntention)(-1);
        sut.IsAllowed(new Mock<IIdentity>().Object, intention).Should().BeFalse();
    }


    [Fact]
    public void ReturnFalse_WhenCheckingAccountIntentionSignOut_AndUserIsGuest()
    {
        sut.IsAllowed(User.Guest, AccountIntention.SignOut).Should().BeFalse();
    }

    [Fact]
    public void ReturnTrue_WhenCheckingAccountIntentionSignOut_AndUserIsAuthenticated()
    {
        sut.IsAllowed(
                new User(Guid.Parse("747d3fd5-0241-4b00-a970-6d4e9b3f15e1"), Guid.Empty),
                AccountIntention.SignOut)
            .Should().BeTrue();
    }
}