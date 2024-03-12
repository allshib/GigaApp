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
using GigaApp.Domain.UseCases.CreateForum;

namespace GigaApp.Domain.UseCases.CreateTopic
{
    internal class CreateTopicUseCase : IRequestHandler<CreateTopicCommand, Topic>
    {
        private readonly IIntentionManager intentionManager;
        private readonly IGetForumsStorage getForumsStorage;
        private readonly IIdentityProvider identityProvider;
        private readonly IUnitOfWork unitOfWork;

        public CreateTopicUseCase(
            IIntentionManager intentionManager,
            IGetForumsStorage getForumsStorage,
            IIdentityProvider identityProvider,
            IUnitOfWork unitOfWork)
        {
            this.intentionManager = intentionManager;
            
            this.getForumsStorage = getForumsStorage;
            this.identityProvider = identityProvider;
            this.unitOfWork = unitOfWork;
        }


        public async Task<Topic> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            intentionManager.ThrowIfForbidden(TopicIntention.Create);

            await getForumsStorage.ThrowIfForumWasNotFound(request.ForumId, cancellationToken);

            await using var scope = await unitOfWork.StartScope();
            var createTopicStorage = scope.GetStorage<ICreateTopicStorage>();
            var createForumStorage = scope.GetStorage<ICreateForumStorage>();

            var topic = await createTopicStorage.CreateTopic(request.ForumId, identityProvider.Current.UserId, request.Title, cancellationToken);
            var forum = await createForumStorage.Create("Test", cancellationToken);

            await scope.Commit(cancellationToken);
            return topic;

        }
    }
}
