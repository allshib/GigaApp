using GigaApp.Domain;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaApp.Storage
{
    internal class UnitOfWork(IServiceProvider serviceProvider) : IUnitOfWork
    {
        public async Task<IUnitOfWorkScope> StartScope(CancellationToken cancellationToken)
        {
            var scope = serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ForumDbContext>();
            var transaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
            return new UnitOfWorkScope(scope, transaction);
        }
    }
    internal class UnitOfWorkScope(IServiceScope scope, IDbContextTransaction transaction) : IUnitOfWorkScope
    {
        public Task Commit(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        

        public TStorage GetStorage<TStorage>() where TStorage : IStorage
        {
            return scope.ServiceProvider.GetRequiredService<TStorage>();
        }
        public async ValueTask DisposeAsync()
        {
            if (scope is IAsyncDisposable scopeAsyncDisposable)
            {
                await scopeAsyncDisposable.DisposeAsync();
            }
            else
                scope.Dispose();
            await transaction.DisposeAsync();
        }
    }
}
