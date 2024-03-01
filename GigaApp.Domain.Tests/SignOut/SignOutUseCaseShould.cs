using FluentAssertions;
using GigaApp.Domain.Authentication;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.Exceptions;
using GigaApp.Domain.Identity;
using GigaApp.Domain.UseCases.SignOut;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Moq;
using Moq.Language.Flow;

namespace GigaApp.Domain.Tests.SignOut;

public class SignOutUseCaseShould
{
    private readonly ISetup<IIntentionManager, bool> signOutIsAllowedSetup;
    private readonly SignOutUseCase sut;
    private readonly ISetup<ISignOutStorage, Task> removeSessionSetup;
    private readonly Mock<ISignOutStorage> storage;
    private readonly ISetup<IIdentityProvider, IIdentity> currentIdentitySetup;


    public SignOutUseCaseShould()
    {

        storage = new Mock<ISignOutStorage>();
        var identityProvider = new Mock<IIdentityProvider>();

        currentIdentitySetup = identityProvider.Setup(x => x.Current);

        removeSessionSetup = storage.Setup(s => s.RemoveSession(It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()));
        var intetntionManager = new Mock<IIntentionManager>();
        signOutIsAllowedSetup = intetntionManager.Setup(x => x.IsAllowed(It.IsAny<AccountIntention>()));

        sut = new SignOutUseCase(identityProvider.Object,
            intetntionManager.Object,
            storage.Object);
    }

    [Fact]
    public void ThrowIntentionManagerException_WhenUserIsNotAuthenticated()
    {
        currentIdentitySetup.Returns(User.Guest);
        signOutIsAllowedSetup.Returns(false);
        sut.Invoking(x=>x.Execute(new SignOutCommand(), CancellationToken.None))
            .Should().ThrowAsync<IntetntionManagerExeption>();
    }

    [Fact]
    public async Task RemoveCurrentIdentitySession()
    {
        var sessionId = Guid.NewGuid();

        removeSessionSetup.Returns(Task.CompletedTask);
        currentIdentitySetup.Returns(new User(Guid.NewGuid(), sessionId));
        signOutIsAllowedSetup.Returns(true);

        await sut.Execute(new SignOutCommand(), CancellationToken.None);

        storage.Verify(s => s.RemoveSession(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        storage.VerifyNoOtherCalls();
    }

}