using GigaApp.Domain.UseCases.SignOut;
using GigaApp.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace GigaApp.Storage.Storages;

internal class SignOutStorage : ISignOutStorage
{
    private readonly ForumDbContext dbContext;
    public SignOutStorage(
        ForumDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task RemoveSession(Guid sessionId, CancellationToken cancellationToken)
    {
        var session = await dbContext.Sessions
            .SingleOrDefaultAsync(s => s.SessionId == sessionId, cancellationToken);

        if (session is not null)
        {
            dbContext.Sessions.Remove(session);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}