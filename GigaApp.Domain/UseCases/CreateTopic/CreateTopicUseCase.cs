using FluentValidation;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.Exceptions;
using GigaApp.Domain.Identity;
using GigaApp.Domain.Models;
using GigaApp.Domain.UseCases.GetForums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using GigaApp.Domain.Monitoring;
using MediatR;
using Topic = GigaApp.Domain.Models.Topic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GigaApp.Domain.UseCases.CreateTopic
{
    internal class CreateTopicUseCase : IRequestHandler<CreateTopicCommand, Topic>
    {
        private readonly IIntentionManager intentionManager;
        private readonly ICreateTopicStorage storage;
        private readonly IGetForumsStorage getForumsStorage;
        private readonly IIdentityProvider identityProvider;
        private readonly IValidator<CreateTopicCommand> validator;
        private readonly DomainMetrics metrics;

        public CreateTopicUseCase(
            IIntentionManager intentionManager,
            ICreateTopicStorage createTopicstorage, 
            IGetForumsStorage getForumsStorage,
            IIdentityProvider identityProvider,
            IValidator<CreateTopicCommand> validator,
            DomainMetrics metrics)
        {
            this.intentionManager = intentionManager;
            this.storage = createTopicstorage;
            this.getForumsStorage = getForumsStorage;
            this.identityProvider = identityProvider;
            this.validator = validator;
            this.metrics = metrics;
        }


        public async Task<Topic> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {

            await validator.ValidateAndThrowAsync(request, cancellationToken);
            intentionManager.ThrowIfForbidden(TopicIntention.Create);

            await getForumsStorage.ThrowIfForumWasNotFound(request.ForumId, cancellationToken);
            var topic = await storage.CreateTopic(request.ForumId, identityProvider.Current.UserId, request.Title, cancellationToken);


            return topic;

        }
    }
}
