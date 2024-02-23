using FluentValidation;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.Exceptions;
using GigaApp.Domain.Identity;
using GigaApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Topic = GigaApp.Domain.Models.Topic;

namespace GigaApp.Domain.UseCases.CreateTopic
{
    internal class CreateTopicUseCase : ICreateTopicUseCase
    {
        private readonly IIntentionManager intentionManager;
        private readonly ICreateTopicStorage storage;
        private readonly IIdentityProvider identityProvider;
        private readonly IValidator<CreateTopicCommand> validator;

        public CreateTopicUseCase(
            IIntentionManager intentionManager,
            ICreateTopicStorage storage, 
            IIdentityProvider identityProvider,
            IValidator<CreateTopicCommand> validator)
        {
            this.intentionManager = intentionManager;
            this.storage = storage;
            this.identityProvider = identityProvider;
            this.validator = validator;
        }

        public async Task<Topic> Execute(CreateTopicCommand command, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            intentionManager.ThrowIfForbidden(TopicIntention.Create);

            var forumExists = await storage.ForumExists(command.ForumId, cancellationToken);
            if (!forumExists)
            {
                throw new ForumNotFoundException(command.ForumId);
            }
            
            return await storage.CreateTopic(command.ForumId, identityProvider.Current.UserId, command.Title, cancellationToken);
        }
    }
}
