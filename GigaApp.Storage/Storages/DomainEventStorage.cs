using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GigaApp.Domain.UseCases;
using GigaApp.Storage.Entities;

namespace GigaApp.Storage.Storages
{
    internal class DomainEventStorage(ForumDbContext dbContext, IGuidFactory guidFactory) : IDomainEventStorage
    {
        public async Task AddEvent<TDomainEntity>(TDomainEntity entity, CancellationToken cancellationToken)
        {
            await dbContext.DomainEvents.AddAsync(new DomainEvent
            {
                DomainEventId = guidFactory.Create(),
                EmittedAt = DateTimeOffset.Now,
                ContentBlob = JsonSerializer.SerializeToUtf8Bytes(entity)
            }, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
