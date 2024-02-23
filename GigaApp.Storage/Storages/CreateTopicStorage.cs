using GigaApp.Domain.UseCases;
using GigaApp.Domain.UseCases.CreateTopic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Storage.Storages
{
    internal class CreateTopicStorage : ICreateTopicStorage
    {
        private readonly IMemoryCache cache;
        private readonly IGuidFactory guidFactory;
        private readonly IMomentProvider momentProvider;
        private readonly ForumDbContext dbContext;

        public CreateTopicStorage(
            IMemoryCache cache,
            IGuidFactory guidFactory,
            IMomentProvider momentProvider,
            ForumDbContext forumDbContext)
        {
            this.cache = cache;
            this.guidFactory = guidFactory;
            this.momentProvider = momentProvider;
            this.dbContext = forumDbContext;
        }

        public async Task<Domain.Models.Topic> CreateTopic(Guid forumId, Guid userId, string title, CancellationToken cancellationToken)
        {
            var topicId = guidFactory.Create();
            var topic = new Topic
            {
                TopicId = topicId,
                ForumId = forumId,
                UserId = userId,
                Title = title,
                CreatedAt = momentProvider.Now,
            };
            await dbContext.Topics.AddAsync(topic, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            var result = await dbContext.Topics
                .Where(t => t.TopicId == topicId)
                .Select(t => new Domain.Models.Topic
                {
                    Id = t.TopicId,
                    ForumId = t.ForumId,
                    UserId = t.UserId,
                    Title = t.Title,
                    CreatedAt = t.CreatedAt,
                }).FirstOrDefaultAsync(cancellationToken);

            return result;
        }

        public async Task<bool> ForumExists(Guid forumId, CancellationToken cancellationToken)
        {
            return await dbContext.Forums.AnyAsync(f=>f.ForumId == forumId, cancellationToken);
        }
    }
}
