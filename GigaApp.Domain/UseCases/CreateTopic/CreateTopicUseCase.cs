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
    internal class CreateTopicUseCase(
        IIntentionManager intentionManager,
        IGetForumsStorage getForumsStorage,
        IIdentityProvider identityProvider,
        IUnitOfWork       unitOfWork): IRequestHandler<CreateTopicCommand, Topic>
    {
        public async Task<Topic> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
        {
            intentionManager.ThrowIfForbidden(TopicIntention.Create);

            await getForumsStorage.ThrowIfForumWasNotFound(request.ForumId, cancellationToken);

            await using var scope = await unitOfWork.StartScope(cancellationToken);
            var createTopicStorage = scope.GetStorage<ICreateTopicStorage>();
            var domainEventStorage = scope.GetStorage<IDomainEventStorage>();


            var topic = await createTopicStorage.CreateTopic(request.ForumId, identityProvider.Current.UserId, request.Title, cancellationToken);
            await domainEventStorage.AddEvent(topic, cancellationToken);

            await scope.Commit(cancellationToken);
            return topic;

        }
    }
}
