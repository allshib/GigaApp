using GigaApp.Domain.Authorization;
using GigaApp.Domain.Identity;

namespace GigaApp.Domain.UseCases.CreateForum
{
    internal class ForumIntentionResolver : IIntentionResolver<ForumIntention>
    {
        public bool IsAllowed(IIdentity subject, ForumIntention intention)
        {
            return intention switch
            {
                ForumIntention.Create => subject.IsAuthenticated(),
                _ => false
            };

        }
    }
}
