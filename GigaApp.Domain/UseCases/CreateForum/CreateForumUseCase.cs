using GigaApp.Domain.Authorization;
using GigaApp.Domain.Models;
using MediatR;

namespace GigaApp.Domain.UseCases.CreateForum
{
    internal class CreateForumUseCase : IRequestHandler<CreateForumCommand, Forum>
    {
        private readonly IIntentionManager intentionManager;
        private readonly ICreateForumStorage storage;


        public CreateForumUseCase(
            IIntentionManager intentionManager,
            ICreateForumStorage storage)
        {
            this.intentionManager = intentionManager;
            this.storage = storage;
        }

        public async Task<Forum> Handle(CreateForumCommand request, CancellationToken cancellationToken)
        {
            intentionManager.ThrowIfForbidden(ForumIntention.Create);

            var forum = await storage.Create(request.Title, cancellationToken);

            return forum;

        }
    }
}
