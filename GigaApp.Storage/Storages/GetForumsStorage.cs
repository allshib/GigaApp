using AutoMapper;
using AutoMapper.QueryableExtensions;
using GigaApp.Domain.UseCases.GetForums;
using GigaApp.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Storage.Storages
{
    internal class GetForumsStorage : IGetForumsStorage
    {
        private readonly IMemoryCache cache;
        private readonly ForumDbContext dbContext;
        private readonly IMapper mapper;

        public GetForumsStorage(
            IMemoryCache cache, 
            ForumDbContext dbContext,
            IMapper mapper)
        {
            this.cache = cache;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<Domain.Models.Forum>> GetForums(CancellationToken cancellationToken)
        {
            return await cache.GetOrCreateAsync(
                nameof(GetForums), 
                entry => {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

                    return dbContext.Forums
                    .ProjectTo<Domain.Models.Forum>(mapper.ConfigurationProvider)
                    .ToArrayAsync(cancellationToken);
                }
            );
        }

        public IEnumerable<Domain.Models.Forum> GetForumsNotAsync()
        {
            return cache.GetOrCreate(
                nameof(GetForums),
                entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);

                    return dbContext.Forums
                        .ProjectTo<Domain.Models.Forum>(mapper.ConfigurationProvider)
                        .ToArray();
                });
        }
    }
}
