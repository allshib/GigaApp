using GigaApp.Domain.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GigaApp.Domain.Monitoring;
using Forum = GigaApp.Domain.Models.Forum;


namespace GigaApp.Domain.UseCases.GetForums
{
    internal class GetForumUseCase : IGetForumsUseCase
    {
        private readonly IGetForumsStorage getForumsStorage;
        private readonly DomainMetrics metrics;
        public GetForumUseCase(IGetForumsStorage getForumsStorage, 
            DomainMetrics metrics)
        {
            this.getForumsStorage = getForumsStorage;
            this.metrics = metrics;
        }

        public async Task<IEnumerable<Forum>> Execute(CancellationToken cancellationToken)
        {
            try
            {
                var forums = await getForumsStorage.GetForums(cancellationToken);
                metrics.ForumsFetched(true);
                return forums;
            }
            catch
            {
                metrics.ForumsFetched(false);
                throw;
            }
        }

    }
}
