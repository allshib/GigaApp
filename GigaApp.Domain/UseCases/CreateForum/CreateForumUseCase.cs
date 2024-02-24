using FluentValidation;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.Models;

namespace GigaApp.Domain.UseCases.CreateForum
{
    public class CreateForumUseCase : ICreateForumUseCase
    {
        private readonly IValidator<CreateForumCommand> validator;
        private readonly IIntentionManager intentionManager;
        private readonly ICreateForumStorage storage;

        public CreateForumUseCase(
            IValidator<CreateForumCommand> validator,
            IIntentionManager intentionManager,
            ICreateForumStorage storage)
        {
            this.validator = validator;
            this.intentionManager = intentionManager;
            this.storage = storage;
        }
        public async Task<Forum> Execute(CreateForumCommand command, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(command, cancellationToken);

            intentionManager.ThrowIfForbidden(ForumIntention.Create);

            return await storage.Create(command.Title, cancellationToken);
        }
    }
}
