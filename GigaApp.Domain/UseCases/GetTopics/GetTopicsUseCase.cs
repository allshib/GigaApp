using GigaApp.Domain.Models;
using GigaApp.Domain.UseCases.GetForums;
using MediatR;

namespace GigaApp.Domain.UseCases.GetTopics
{
    internal class GetTopicsUseCase : IRequestHandler<GetTopicsQuery, (IEnumerable<Topic> resources, int totalCount)>
    {

        private readonly IGetForumsStorage getForumsStorage;
        private readonly IGetTopicsStorage storage;

        public GetTopicsUseCase(
            IGetForumsStorage getForumsStorage,
            IGetTopicsStorage getTopicsStorage)
        {
            this.getForumsStorage = getForumsStorage;
            this.storage = getTopicsStorage;
        }



        public async Task<(IEnumerable<Topic> resources, int totalCount)> Handle(GetTopicsQuery request, CancellationToken cancellationToken)
        {
            await getForumsStorage.ThrowIfForumWasNotFound(request.ForumId, cancellationToken);

            return await storage.GetTopics(request.ForumId, request.Skip, request.Take, cancellationToken);
        }
    }
}
