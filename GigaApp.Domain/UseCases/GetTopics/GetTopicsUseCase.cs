using FluentValidation;
using GigaApp.Domain.Models;
using GigaApp.Domain.UseCases.GetForums;

namespace GigaApp.Domain.UseCases.GetTopics
{
    internal class GetTopicsUseCase : IGetTopicsUseCase
    {

        private readonly IValidator<GetTopicsQuery> validator;
        private readonly IGetForumsStorage getForumsStorage;
        private readonly IGetTopicsStorage storage;

        public GetTopicsUseCase(
            IValidator<GetTopicsQuery> validator,
            IGetForumsStorage getForumsStorage,
            IGetTopicsStorage getTopicsStorage)
        {
            this.validator = validator;
            this.getForumsStorage = getForumsStorage;
            this.storage = getTopicsStorage;
        }

        public async Task<(IEnumerable<Topic> resources, int totalCount)> Execute(
            GetTopicsQuery query, CancellationToken cancellationToken)
        {

            await validator.ValidateAndThrowAsync(query, cancellationToken);

            await getForumsStorage.ThrowIfForumWasNotFound(query.ForumId, cancellationToken);

            return await storage.GetTopics(query.ForumId, query.Skip, query.Take, cancellationToken);
        }
    }
}
