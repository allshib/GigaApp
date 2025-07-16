using GigaApp.Domain.UseCases.GetTopics;
using Microsoft.EntityFrameworkCore;

namespace GigaApp.Storage.Storages
{
    internal class GetTopicsStorage : IGetTopicsStorage
    {
        private readonly ForumDbContext forumDbContext;

        public GetTopicsStorage(ForumDbContext forumDbContext)
        {
            this.forumDbContext = forumDbContext;
        }

        public async Task<(IEnumerable<Domain.Models.Topic> topics, int totalCount)> GetTopics(Guid forumId, int skip, int take, CancellationToken cancellationToken)
        {
            var query = forumDbContext.Topics.Where(x=>x.ForumId == forumId);

            var totalCount = await query.CountAsync(cancellationToken);

            var resources = await query.Select(t => new Domain.Models.Topic
                {
                    ForumId = forumId,
                    Id = t.TopicId,
                    Title = t.Title,
                    CreatedAt = t.CreatedAt,
                    UserId = t.UserId,
                })
                .Skip(skip)
                .Take(take)
                .ToArrayAsync(cancellationToken);

            return (resources, totalCount);
        }
    }
}
