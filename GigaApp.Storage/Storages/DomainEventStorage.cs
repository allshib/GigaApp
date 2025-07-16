using System.Text.Json;
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
