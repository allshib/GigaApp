using GigaApp.Domain.Authorization;
using GigaApp.Domain.Identity;

namespace GigaApp.Domain.UseCases.SignOut;

public class SignOutUseCase : ISignOutUseCase
{
    private readonly IIdentityProvider identityProvider;
    private readonly IIntentionManager intentionManager;
    private readonly ISignOutStorage storage;

    public SignOutUseCase(
        IIdentityProvider identityProvider, 
        IIntentionManager intentionManager,
        ISignOutStorage storage)
    {
        this.identityProvider = identityProvider;
        this.intentionManager = intentionManager;
        this.storage = storage;
    }
    public async Task Execute(SignOutCommand command, CancellationToken cancellationToken)
    {
        intentionManager.ThrowIfForbidden(AccountIntention.SignOut);

        var sessionId = identityProvider.Current.SessionId;
        await storage.RemoveSession(sessionId, cancellationToken);
    }
}