﻿using GigaApp.Domain.UseCases.GetForums;
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

        public GetForumsStorage(IMemoryCache cache, ForumDbContext dbContext)
        {
            this.cache = cache;
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Domain.Models.Forum>> GetForums(CancellationToken cancellationToken)
        {
            return await cache.GetOrCreateAsync(
                nameof(GetForums), 
                entry => {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(10);
                    return dbContext.Forums
                    .Select(f => new Domain.Models.Forum
                    {
                        Id = f.ForumId,
                        Title = f.Title,
                    })
                    .ToArrayAsync(cancellationToken);
                }
            );
        }
    }
}
