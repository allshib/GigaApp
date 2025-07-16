using AutoMapper;
using GigaApp.Domain.Models;
using GigaApp.Domain.UseCases.UpdateForumUseCase;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GigaApp.Storage.Storages
{
    internal class UpdateForumStorage(ForumDbContext dbContext, IMapper mapper, IMemoryCache cache) : IUpdateForumStorage
    {
        public async Task Update(Forum forum, CancellationToken cancellationToken)
        {
            var dbForum = await dbContext.Forums.FirstOrDefaultAsync(x => x.ForumId == forum.Id, cancellationToken);

            mapper.Map(forum, dbForum);

            await dbContext.SaveChangesAsync(cancellationToken);

            cache.Remove(nameof(GetForumsStorage.GetForums));
        }
    }
}
