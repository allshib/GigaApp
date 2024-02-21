using GigaApp.Domain.Authorization;
using GigaApp.Domain.Exceptions;
using GigaApp.Domain.Identity;
using GigaApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Topic = GigaApp.Domain.Models.Topic;

namespace GigaApp.Domain.UseCases.CreateTopic
{
    public class CreateTopicUseCase : ICreateTopicUseCase
    {
        private readonly IIntentionManager intentionManager;
        private readonly ICreateTopicStorage storage;
        private readonly IIdentityProvider identityProvider;

        public CreateTopicUseCase(
            IIntentionManager intentionManager,
            ICreateTopicStorage storage, 
            IIdentityProvider identityProvider)
        {
            this.intentionManager = intentionManager;
            this.storage = storage;
            this.identityProvider = identityProvider;
        }

        public async Task<Topic> Execute(Guid forumId, string title, CancellationToken cancellationToken)
        {
            intentionManager.ThrowIfForbidden(TopicIntention.Create);

            var forumExists = await storage.ForumExists(forumId, cancellationToken);
            if (!forumExists)
            {
                throw new ForumNotFoundException(forumId);
            }
            
            return await storage.CreateTopic(forumId, identityProvider.Current.UserId, title, cancellationToken);
        }
    }
}
