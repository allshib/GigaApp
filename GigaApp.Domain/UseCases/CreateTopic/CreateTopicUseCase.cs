using GigaApp.Domain.Authorization;
using GigaApp.Domain.Identity;
using GigaApp.Domain.UseCases.GetForums;
using MediatR;
using Topic = GigaApp.Domain.Models.Topic;

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
