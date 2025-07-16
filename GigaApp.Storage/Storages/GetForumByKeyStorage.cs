using AutoMapper;
using AutoMapper.QueryableExtensions;
using GigaApp.Domain.UseCases.GetForumByKey;
using Microsoft.EntityFrameworkCore;
using Forum = GigaApp.Domain.Models.Forum;

namespace GigaApp.Storage.Storages
{
    internal class GetForumByKeyStorage(ForumDbContext dbContext, IMapper mapper) : IGetForumByKeyStorage
    {
        public async Task<Forum?> GetForumByKey(Guid id, CancellationToken cancellationToken)
        {
            return await dbContext.Forums
                .Where(t => t.ForumId == id)
                .ProjectTo<Forum>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
