using GigaApp.Domain.Authorization;
using GigaApp.Domain.Identity;

namespace GigaApp.Domain.UseCases.CreateTopic
{
    /// <summary>
    /// Разрешения, связанные с Topic
    /// </summary>
    internal class TopicIntetntionResolver : IIntentionResolver<TopicIntention>
    {
        public bool IsAllowed(IIdentity subject, TopicIntention intention) => intention switch
        {
            TopicIntention.Create => subject.IsAuthenticated(),
            _ => false
        };
        
    }
}
