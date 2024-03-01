using GigaApp.Domain.Authentication;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.Identity;

namespace GigaApp.Domain.UseCases.SignOut;

internal class AccountIntentionResolver : IIntentionResolver<AccountIntention>
{
    public bool IsAllowed(IIdentity subject, AccountIntention intention)
    {
        return intention switch
        {
            AccountIntention.SignOut => subject.IsAuthenticated(),
            _ => false
        };
    }
}