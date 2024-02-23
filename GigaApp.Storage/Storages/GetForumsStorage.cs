using GigaApp.Domain.UseCases.GetForums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Storage.Storages
{
    internal class GetForumsStorage : IGetForumsStorage
    {
        private readonly ForumDbContext dbContext;

        public GetForumsStorage(ForumDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<Domain.Models.Forum>> GetForums(CancellationToken cancellationToken)
        {
            return await dbContext.Forums
                .Select(f=> new Domain.Models.Forum
                {
                    Id = f.ForumId,
                    Title = f.Title,
                }).ToArrayAsync(cancellationToken);
        }
    }
}
