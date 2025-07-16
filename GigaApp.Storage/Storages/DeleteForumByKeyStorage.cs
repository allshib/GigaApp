using GigaApp.Domain.UseCases.DeleteForumByKey;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GigaApp.Storage.Storages
{
    internal class DeleteForumByKeyStorage(ForumDbContext forumDbContext, IMemoryCache cache) : IDeleteForumByKeyStorage
    {
        public async Task DeleteByKey(Guid key, CancellationToken cancellationToken)
        {
            var forum = await forumDbContext.Forums.FirstOrDefaultAsync(x => x.ForumId == key, cancellationToken);

            forumDbContext.Forums.Remove(forum);

            await forumDbContext.SaveChangesAsync(cancellationToken);

            cache.Remove(nameof(GetForumsStorage.GetForums));
        }
    }
}
