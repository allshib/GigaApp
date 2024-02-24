using AutoMapper;
using AutoMapper.QueryableExtensions;
using GigaApp.Domain.UseCases;
using GigaApp.Domain.UseCases.CreateForum;
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
    internal class CreateForumStorage : ICreateForumStorage
    {
        private readonly IMemoryCache cache;
        private readonly IGuidFactory guidFactory;
        private readonly ForumDbContext dbContext;
        private readonly IMapper mapper;

        public CreateForumStorage(
            IMemoryCache cache,
            IGuidFactory guidFactory,
            ForumDbContext dbContext,
            IMapper mapper)
        {
            this.cache = cache;
            this.guidFactory = guidFactory;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<Domain.Models.Forum> Create(string title, CancellationToken cancellationToken)
        {
            var forumId = guidFactory.Create();
            var forum = new Forum
            {
                ForumId = forumId,
                Title = title,
            };

            await dbContext.Forums.AddAsync(forum, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            cache.Remove(nameof(GetForumsStorage.GetForums));

            return await dbContext.Forums.Where(f=>f.ForumId == forumId)
                .ProjectTo<Domain.Models.Forum>(mapper.ConfigurationProvider)
                .FirstAsync(cancellationToken);

        }
    }
}
