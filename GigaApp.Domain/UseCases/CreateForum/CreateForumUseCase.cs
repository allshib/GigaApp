using FluentValidation;
using GigaApp.Domain.Authorization;
using GigaApp.Domain.Models;
using GigaApp.Domain.Monitoring;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace GigaApp.Domain.UseCases.CreateForum
{
    public class CreateForumUseCase : IRequestHandler<CreateForumCommand, Forum>
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

        public async Task<Forum> Handle(CreateForumCommand request, CancellationToken cancellationToken)
        {
            await validator.ValidateAndThrowAsync(request, cancellationToken);
            intentionManager.ThrowIfForbidden(ForumIntention.Create);

            var forum = await storage.Create(request.Title, cancellationToken);

            return forum;

        }
    }
}
